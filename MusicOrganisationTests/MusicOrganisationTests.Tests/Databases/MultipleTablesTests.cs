using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Databases;

namespace MusicOrganisationTests.Tests.Databases
{
    public class MultipleTablesTests
    {
        [Fact]
        public async void TestMultipleTables()
        {
            TableConnection<Composer> composerDatabase = new(nameof(TestMultipleTables));
            TableConnection<Work> workDatabase = new(nameof(TestMultipleTables));

            await composerDatabase.ClearDataAsync();
            await workDatabase.ClearDataAsync();

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
            TableConnection<Composer> composerDatabase = new(nameof(TestClearOneTable));
            TableConnection<Work> workDatabase = new(nameof(TestClearOneTable));

            await composerDatabase.ClearDataAsync();
            await workDatabase.ClearDataAsync();

            await composerDatabase.InsertAllAsync(Expected.Composers);
            await workDatabase.InsertAllAsync(Expected.Works);

            await composerDatabase.ClearDataAsync();

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