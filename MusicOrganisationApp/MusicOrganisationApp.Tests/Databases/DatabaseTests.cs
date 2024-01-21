using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Tests.Databases
{
    public class DatabaseTests
    {
        [Fact]
        public async Task TestInsertAsync()
        {
            DatabaseConnection database = new(nameof(TestInsertAsync));
            await database.DropTableAsync<ComposerData>();
            await database.InsertAsync(Expected.ComposerData[0]);
            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.ComposerData[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            DatabaseConnection database = new(nameof(TestClearDataAsync));
            await database.DropTableAsync<WorkData>();
            await database.InsertAllAsync(Expected.WorkData);
            await database.ClearTableAsync<WorkData>();

            IEnumerable<WorkData> actualComposers = await database.GetAllAsync<WorkData>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            DatabaseConnection database = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await database.DropTableAsync<ComposerData>();
            await database.InsertAllAsync(Expected.ComposerData);
            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();

            Assert.Equal(Expected.ComposerData.Count, actualComposers.Count());
            foreach (ComposerData expectedComposer in Expected.ComposerData)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public async Task TestSucessfulTryGetAsnyc()
        {
            DatabaseConnection database = new(nameof(TestSucessfulTryGetAsnyc));
            await database.DropTableAsync<WorkData>();
            await database.InsertAllAsync(Expected.WorkData);
            (bool found, WorkData actualWork) = await database.TryGetAsync<WorkData>(Expected.WorkData[0].Id);
            Assert.True(found);
            Assert.Equal(Expected.WorkData[0], actualWork);
        }

        [Fact]
        public async Task TestUnsucessfulTryGetAsync()
        {
            DatabaseConnection database = new(nameof(TestUnsucessfulTryGetAsync));
            await database.DropTableAsync<WorkData>();
            await database.InsertAllAsync(Expected.WorkData);
            (bool found, WorkData actualWork) = await database.TryGetAsync<WorkData>(-1);
            Assert.False(found);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            DatabaseConnection database = new(nameof(TestDeleteAsync));
            await database.ClearTableAsync<ComposerData>();
            await database.InsertAllAsync(Expected.ComposerData);
            await database.DeleteAsync(Expected.ComposerData[0]);

            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            Assert.DoesNotContain(Expected.ComposerData[0], actualComposers);
            for (int i = 1; i < Expected.ComposerData.Count; ++i)
            {
                Assert.Contains(Expected.ComposerData[i], actualComposers);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            DatabaseConnection database = new(nameof(TestUpdateAsync));
            await database.ClearTableAsync<WorkData>();
            await database.InsertAllAsync(Expected.WorkData);
            WorkData updatedWork = new()
            {
                Id = Expected.WorkData[0].Id,
                ComposerId = Expected.WorkData[0].ComposerId,
                Title = "Something different",
                Subtitle = "New subtitle",
                Genre = "Different genre"
            };
            await database.UpdateAsync(updatedWork);
            IEnumerable<WorkData> actualWorks = await database.GetAllAsync<WorkData>();
            Assert.DoesNotContain(Expected.WorkData[0], actualWorks);
            for (int i = 1; i < Expected.WorkData.Count; ++i)
            {
                Assert.Contains(Expected.WorkData[i], actualWorks);
            }
        }

        [Fact]
        public async Task TestGetIdsAsync()
        {
            DatabaseConnection database = new(nameof(TestGetIdsAsync));
            await database.ClearTableAsync<ComposerData>();
            await database.InsertAllAsync(Expected.ComposerData);
            IEnumerable<int> actualIds = await database.GetIdsAsync<ComposerData>();
            foreach (ComposerData expectedComposer in Expected.ComposerData)
            {
                Assert.Contains(expectedComposer.Id, actualIds);
            }
        }

        [Fact]
        public async Task TestNullProperties()
        {
            DatabaseConnection database = new(nameof(TestNullProperties));
            await database.ClearTableAsync<ComposerData>();
            await database.InsertAsync(Expected.NullPropertyComposer);
            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            DatabaseConnection database = new(nameof(TestGetNextIdAsync));
            await database.ClearTableAsync<WorkData>();
            await database.InsertAllAsync(Expected.WorkData);
            int nextId = await database.GetNextIdAsync<WorkData>();
            Assert.Equal(Expected.WorkData.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async void TestMultipleTables()
        {
            DatabaseConnection database = new(nameof(TestMultipleTables));

            await database.ClearTableAsync<ComposerData>();
            await database.ClearTableAsync<WorkData>();

            await database.InsertAllAsync(Expected.ComposerData);
            await database.InsertAllAsync(Expected.WorkData);

            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await database.GetAllAsync<WorkData>();

            foreach (ComposerData expectedComposer in Expected.ComposerData)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }

            foreach (WorkData expectedWork in Expected.WorkData)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async void TestClearOneTable()
        {
            DatabaseConnection database = new(nameof(TestClearOneTable));

            await database.ClearTableAsync<ComposerData>();
            await database.ClearTableAsync<WorkData>();

            await database.InsertAllAsync(Expected.ComposerData);
            await database.InsertAllAsync(Expected.WorkData);

            await database.ClearTableAsync<ComposerData>();

            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await database.GetAllAsync<WorkData>();

            Assert.Empty(actualComposers);

            foreach (WorkData expectedWork in Expected.WorkData)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}