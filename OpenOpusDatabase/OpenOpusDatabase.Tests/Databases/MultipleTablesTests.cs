using OpenOpusDatabase.Lib.Databases;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests.Databases
{
    public class MultipleTablesTests
    {
        [Fact]
        public async void TestAddComposersAndWorks()
        {
            ComposerDatabase composerDatabase = new(nameof(TestAddComposersAndWorks));
            WorkDatabase workDatabase = new(nameof(TestAddComposersAndWorks));

            await composerDatabase.ClearAsync();
            await workDatabase.ClearAsync();

            await composerDatabase.InsertAllAsync(Expected.Composers);
            await workDatabase.InsertAllAsync(Expected.Works);

            List<Composer> actualComposers = await composerDatabase.GetAllAsync();
            List<Work> actualWorks = await workDatabase.GetAllAsync();

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
            ComposerDatabase composerDatabase = new(nameof(TestClearOneTable));
            WorkDatabase workDatabase = new(nameof(TestClearOneTable));

            await composerDatabase.ClearAsync();
            await workDatabase.ClearAsync();

            await composerDatabase.InsertAllAsync(Expected.Composers);
            await workDatabase.InsertAllAsync(Expected.Works);

            await composerDatabase.ClearAsync();

            List<Composer> actualComposers = await composerDatabase.GetAllAsync();
            List<Work> actualWorks = await workDatabase.GetAllAsync();

            Assert.Empty(actualComposers);

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}