using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Databases;

namespace MusicOrganisationTests.Tests.Databases
{
    public class MultipleTablesTests
    {
        [Fact]
        public async void TestAddComposersAndWorks()
        {
            Table<Composer> composerDatabase = new(nameof(TestAddComposersAndWorks));
            Table<Work> workDatabase = new(nameof(TestAddComposersAndWorks));

            await composerDatabase.ClearAsync();
            await workDatabase.ClearAsync();

            await composerDatabase.InsertAllAsync(Expected.Composers);
            await workDatabase.InsertAllAsync(Expected.Works);

            IEnumerable<Composer> actualComposers = await composerDatabase.GetAllAsync();
            IEnumerable<Work> actualWorks = await workDatabase.GetAllAsync();

            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async void TestClearOneTable()
        {
            Table<Composer> composerDatabase = new(nameof(TestClearOneTable));
            Table<Work> workDatabase = new(nameof(TestClearOneTable));

            await composerDatabase.ClearAsync();
            await workDatabase.ClearAsync();

            await composerDatabase.InsertAllAsync(Expected.Composers);
            await workDatabase.InsertAllAsync(Expected.Works);

            await composerDatabase.ClearAsync();

            IEnumerable<Composer> actualComposers = await composerDatabase.GetAllAsync();
            IEnumerable<Work> actualWorks = await workDatabase.GetAllAsync();

            Assert.Empty(actualComposers);

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}