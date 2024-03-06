using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Databases
{
    public class DatabaseTests
    {
        [Fact]
        public async Task TestInsertAsync()
        {
            DatabaseConnection database = new(nameof(TestInsertAsync));
            await database.DropTableIfExistsAsync<Composer>();
            await database.InsertAsync(ExpectedTables.Composers[0]);
            IEnumerable<Composer> actualComposers = await database.GetAllAsync<Composer>();
            Assert.Single(actualComposers);
            Assert.Contains(ExpectedTables.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            DatabaseConnection database = new(nameof(TestClearDataAsync));
            await database.ResetTableAsync(ExpectedTables.WorkData);
            await database.ClearTableAsync<WorkData>();

            IEnumerable<WorkData> actualComposers = await database.GetAllAsync<WorkData>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            DatabaseConnection database = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await database.ResetTableAsync(ExpectedTables.Composers);
            IEnumerable<Composer> actualComposers = await database.GetAllAsync<Composer>();

            CollectionAssert.EqualContents(ExpectedTables.Composers, actualComposers);
        }

        [Fact]
        public async Task TestSucessfulTryGetAsnyc()
        {
            DatabaseConnection database = new(nameof(TestSucessfulTryGetAsnyc));
            await database.ResetTableAsync(ExpectedTables.WorkData);
            (bool found, WorkData actualWork) = await database.TryGetAsync<WorkData>(ExpectedTables.WorkData[0].Id);
            Assert.True(found);
            Assert.Equal(ExpectedTables.WorkData[0], actualWork);
        }

        [Fact]
        public async Task TestUnsucessfulTryGetAsync()
        {
            DatabaseConnection database = new(nameof(TestUnsucessfulTryGetAsync));
            await database.ResetTableAsync(ExpectedTables.WorkData);
            (bool found, WorkData actualWork) = await database.TryGetAsync<WorkData>(-1);
            Assert.False(found);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            DatabaseConnection database = new(nameof(TestDeleteAsync));
            await database.ResetTableAsync(ExpectedTables.Composers);
            await database.DeleteAsync(ExpectedTables.Composers[0]);

            IEnumerable<Composer> actualComposers = await database.GetAllAsync<Composer>();
            Assert.DoesNotContain(ExpectedTables.Composers[0], actualComposers);
            for (int i = 1; i < ExpectedTables.Composers.Count; ++i)
            {
                Assert.Contains(ExpectedTables.Composers[i], actualComposers);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            DatabaseConnection database = new(nameof(TestUpdateAsync));
            await database.ResetTableAsync(ExpectedTables.WorkData);
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
        public async Task TestNullProperties()
        {
            DatabaseConnection database = new(nameof(TestNullProperties));
            await database.DropTableIfExistsAsync<Composer>();
            await database.InsertAsync(ExpectedTables.NullPropertyComposer);
            IEnumerable<Composer> actualComposers = await database.GetAllAsync<Composer>();
            Assert.Single(actualComposers);
            Assert.Contains(ExpectedTables.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            DatabaseConnection database = new(nameof(TestGetNextIdAsync));
            await database.ResetTableAsync(ExpectedTables.WorkData);
            int actualNextId = await database.GetNextIdAsync<WorkData>();
            int expectedNextId = ExpectedTables.WorkData.Max(w => w.Id) + 1;
            Assert.Equal(expectedNextId, actualNextId);
        }

        [Fact]
        public async Task TestGetNextIdFromEmptyAsync()
        {
            DatabaseConnection database = new(nameof(TestGetNextIdFromEmptyAsync));
            await database.DropTableIfExistsAsync<Composer>();
            int nextId = await database.GetNextIdAsync<Composer>();
            Assert.Equal(0, nextId);
        }

        [Fact]
        public async Task TestMultipleTables()
        {
            DatabaseConnection database = new(nameof(TestMultipleTables));

            await database.ResetTableAsync(ExpectedTables.Composers);
            await database.ResetTableAsync(ExpectedTables.WorkData);

            IEnumerable<Composer> actualComposers = await database.GetAllAsync<Composer>();
            IEnumerable<WorkData> actualWorkData = await database.GetAllAsync<WorkData>();

            CollectionAssert.EqualContents(ExpectedTables.Composers, actualComposers);
            CollectionAssert.EqualContents(ExpectedTables.WorkData, actualWorkData);
        }

        [Fact]
        public async Task TestClearOneTable()
        {
            DatabaseConnection database = new(nameof(TestClearOneTable));

            await database.ResetTableAsync(ExpectedTables.Composers);
            await database.ResetTableAsync(ExpectedTables.WorkData);

            await database.ClearTableAsync<Composer>();

            IEnumerable<Composer> actualComposers = await database.GetAllAsync<Composer>();
            IEnumerable<WorkData> actualWorkData = await database.GetAllAsync<WorkData>();

            Assert.Empty(actualComposers);
            CollectionAssert.EqualContents(ExpectedTables.WorkData, actualWorkData);
        }

        [Fact]
        public async Task TestQueryWithoutCreateTableAsync()
        {
            DatabaseConnection database = new(nameof(TestQueryWithoutCreateTableAsync));
            SqlQuery<Composer> sqlQuery = new() { SelectAll = true };
            IEnumerable<Composer> composers = await database.QueryAsync<Composer>(sqlQuery);
            Assert.Empty(composers);
        }

        [Fact]
        public async Task TestUnsucessfulGetAsnyc()
        {
            DatabaseConnection database = new(nameof(TestUnsucessfulGetAsnyc));
            await database.ResetTableAsync(ExpectedTables.Composers);
            int id = ExpectedTables.Composers.Max(composer => composer.Id) + 1;
            (bool suceeded, Composer actualComposer) = await database.TryGetAsync<Composer>(id);
            Assert.False(suceeded);
            Assert.Equal(new(), actualComposer);
        }

        [Fact]
        public async Task TestSqlInjectionAsync()
        {
            DatabaseConnection database = new(nameof(TestSqlInjectionAsync));
            await database.DropTableIfExistsAsync<Pupil>();
            await database.InsertAsync(ExpectedTables.SqlInjectionPupil);
            IEnumerable<Pupil> actualPupils = await database.GetAllAsync<Pupil>();
            Assert.Single(actualPupils);
            Assert.Contains(ExpectedTables.SqlInjectionPupil, actualPupils);
        }
    }
}