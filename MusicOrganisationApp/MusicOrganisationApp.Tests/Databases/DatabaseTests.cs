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
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.InsertAsync(ExpectedTables.ComposerData[0]);
            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(ExpectedTables.ComposerData[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            DatabaseConnection database = new(nameof(TestClearDataAsync));
            await database.DropTableIfExistsAsync<WorkData>();
            await database.InsertAllAsync(ExpectedTables.WorkData);
            await database.ClearTableAsync<WorkData>();

            IEnumerable<WorkData> actualComposers = await database.GetAllAsync<WorkData>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            DatabaseConnection database = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.InsertAllAsync(ExpectedTables.ComposerData);
            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();

            Assert.Equal(ExpectedTables.ComposerData.Count, actualComposers.Count());
            foreach (ComposerData expectedComposer in ExpectedTables.ComposerData)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public async Task TestSucessfulTryGetAsnyc()
        {
            DatabaseConnection database = new(nameof(TestSucessfulTryGetAsnyc));
            await database.DropTableIfExistsAsync<WorkData>();
            await database.InsertAllAsync(ExpectedTables.WorkData);
            (bool found, WorkData actualWork) = await database.TryGetAsync<WorkData>(ExpectedTables.WorkData[0].Id);
            Assert.True(found);
            Assert.Equal(ExpectedTables.WorkData[0], actualWork);
        }

        [Fact]
        public async Task TestUnsucessfulTryGetAsync()
        {
            DatabaseConnection database = new(nameof(TestUnsucessfulTryGetAsync));
            await database.DropTableIfExistsAsync<WorkData>();
            await database.InsertAllAsync(ExpectedTables.WorkData);
            (bool found, WorkData actualWork) = await database.TryGetAsync<WorkData>(-1);
            Assert.False(found);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            DatabaseConnection database = new(nameof(TestDeleteAsync));
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.InsertAllAsync(ExpectedTables.ComposerData);
            await database.DeleteAsync(ExpectedTables.ComposerData[0]);

            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            Assert.DoesNotContain(ExpectedTables.ComposerData[0], actualComposers);
            for (int i = 1; i < ExpectedTables.ComposerData.Count; ++i)
            {
                Assert.Contains(ExpectedTables.ComposerData[i], actualComposers);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            DatabaseConnection database = new(nameof(TestUpdateAsync));
            await database.DropTableIfExistsAsync<WorkData>();
            await database.InsertAllAsync(ExpectedTables.WorkData);
            WorkData updatedWork = new()
            {
                Id = ExpectedTables.WorkData[0].Id,
                ComposerId = ExpectedTables.WorkData[0].ComposerId,
                Title = "Something different",
                Subtitle = "New subtitle",
                Genre = "Different genre"
            };
            await database.UpdateAsync(updatedWork);
            IEnumerable<WorkData> actualWorks = await database.GetAllAsync<WorkData>();
            Assert.DoesNotContain(ExpectedTables.WorkData[0], actualWorks);
            for (int i = 1; i < ExpectedTables.WorkData.Count; ++i)
            {
                Assert.Contains(ExpectedTables.WorkData[i], actualWorks);
            }
        }

        [Fact]
        public async Task TestGetIdsAsync()
        {
            DatabaseConnection database = new(nameof(TestGetIdsAsync));
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.InsertAllAsync(ExpectedTables.ComposerData);
            IEnumerable<int> actualIds = await database.GetIdsAsync<ComposerData>();
            foreach (ComposerData expectedComposer in ExpectedTables.ComposerData)
            {
                Assert.Contains(expectedComposer.Id, actualIds);
            }
        }

        [Fact]
        public async Task TestNullProperties()
        {
            DatabaseConnection database = new(nameof(TestNullProperties));
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.InsertAsync(ExpectedTables.NullPropertyComposer);
            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(ExpectedTables.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            DatabaseConnection database = new(nameof(TestGetNextIdAsync));
            await database.DropTableIfExistsAsync<WorkData>();
            await database.InsertAllAsync(ExpectedTables.WorkData);
            int nextId = await database.GetNextIdAsync<WorkData>();
            Assert.Equal(ExpectedTables.WorkData.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async Task TestGetNextIdFromEmptyAsync()
        {
            DatabaseConnection database = new(nameof(TestGetNextIdFromEmptyAsync));
            await database.DropTableIfExistsAsync<ComposerData>();
            int nextId = await database.GetNextIdAsync<ComposerData>();
            Assert.Equal(1, nextId);
        }

        [Fact]
        public async void TestMultipleTables()
        {
            DatabaseConnection database = new(nameof(TestMultipleTables));

            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();

            await database.InsertAllAsync(ExpectedTables.ComposerData);
            await database.InsertAllAsync(ExpectedTables.WorkData);

            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await database.GetAllAsync<WorkData>();

            foreach (ComposerData expectedComposer in ExpectedTables.ComposerData)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }

            foreach (WorkData expectedWork in ExpectedTables.WorkData)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async void TestClearOneTable()
        {
            DatabaseConnection database = new(nameof(TestClearOneTable));

            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();

            await database.InsertAllAsync(ExpectedTables.ComposerData);
            await database.InsertAllAsync(ExpectedTables.WorkData);

            await database.ClearTableAsync<ComposerData>();

            IEnumerable<ComposerData> actualComposers = await database.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await database.GetAllAsync<WorkData>();

            Assert.Empty(actualComposers);

            foreach (WorkData expectedWork in ExpectedTables.WorkData)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async Task TestQueryWithoutCreateTableAsync()
        {
            DatabaseConnection database = new(nameof(TestQueryWithoutCreateTableAsync));
            SqlQuery<ComposerData> sqlQuery = new()
            {
                SelectAll = true
            };
            IEnumerable<ComposerData> composers = await database.QueryAsync<ComposerData>(sqlQuery);
            Assert.Empty(composers);
        }
    }
}