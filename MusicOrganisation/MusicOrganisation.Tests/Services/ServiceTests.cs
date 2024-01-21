using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Tests.Services
{
    public class ServiceTests
    {
        [Fact]
        public async Task TestEmptyQueryAsync()
        {
            DatabaseConnection database = new(nameof(TestEmptyQueryAsync));
            await database.QueryAsync<ComposerData>(string.Empty);
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            DatabaseConnection table = new(nameof(TestInsertAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAsync(Expected.ComposerData[0]);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.ComposerData[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            DatabaseConnection table = new(nameof(TestClearDataAsync));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            await table.ClearTableAsync<WorkData>();

            IEnumerable<WorkData> actualComposers = await table.GetAllAsync<WorkData>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            DatabaseConnection table = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAllAsync(Expected.ComposerData);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();

            Assert.Equal(Expected.ComposerData.Count, actualComposers.Count());
            foreach (ComposerData expectedComposer in Expected.ComposerData)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public async Task TestGetAsnyc()
        {
            DatabaseConnection table = new(nameof(TestGetAsnyc));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            WorkData actualWork = await table.GetAsync<WorkData>(Expected.WorkData[0].Id);
            Assert.Equal(Expected.WorkData[0], actualWork);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            DatabaseConnection table = new(nameof(TestDeleteAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAllAsync(Expected.ComposerData);
            await table.DeleteAsync(Expected.ComposerData[0]);

            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.DoesNotContain(Expected.ComposerData[0], actualComposers);
            for (int i = 1; i < Expected.ComposerData.Count; ++i)
            {
                Assert.Contains(Expected.ComposerData[i], actualComposers);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            DatabaseConnection table = new(nameof(TestUpdateAsync));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            WorkData updatedWork = new()
            {
                Id = Expected.WorkData[0].Id,
                ComposerId = Expected.WorkData[0].ComposerId,
                Title = "Something different",
                Subtitle = "New subtitle",
                Genre = "Different genre"
            };
            await table.UpdateAsync(updatedWork);
            IEnumerable<WorkData> actualWorks = await table.GetAllAsync<WorkData>();
            Assert.DoesNotContain(Expected.WorkData[0], actualWorks);
            for (int i = 1; i < Expected.WorkData.Count; ++i)
            {
                Assert.Contains(Expected.WorkData[i], actualWorks);
            }
        }

        [Fact]
        public async Task TestGetIdsAsync()
        {
            DatabaseConnection table = new(nameof(TestGetIdsAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAllAsync(Expected.ComposerData);
            IEnumerable<int> actualIds = await table.GetIdsAsync<ComposerData>();
            foreach (ComposerData expectedComposer in Expected.ComposerData)
            {
                Assert.Contains(expectedComposer.Id, actualIds);
            }
        }

        [Fact]
        public async Task TestNullProperties()
        {
            DatabaseConnection table = new(nameof(TestNullProperties));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAsync(Expected.NullPropertyComposer);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            DatabaseConnection table = new(nameof(TestGetNextIdAsync));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            int nextId = await table.GetNextIdAsync<WorkData>();
            Assert.Equal(Expected.WorkData.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async Task TestGetWhereEqualAsync()
        {
            DatabaseConnection table = new(nameof(TestGetWhereEqualAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAllAsync(Expected.ComposerData);
            IEnumerable<ComposerData> actualComposers = await table.GetWhereEqualAsync<ComposerData>(nameof(ComposerData.Name), Expected.ComposerData[0].Name);
            Assert.Contains(Expected.ComposerData[0], actualComposers);
        }

        [Fact]
        public async Task TestGetWhereTextLikeAsync()
        {
            DatabaseConnection table = new(nameof(TestGetWhereTextLikeAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAllAsync(Expected.ComposerData);
            string expectedText = "ch";
            IEnumerable<ComposerData> actualComposers = await table.GetWhereTextLikeAsync<ComposerData>(nameof(ComposerData.Name), expectedText);
            foreach (ComposerData composer in actualComposers)
            {
                Assert.Contains(expectedText, composer.Name.ToLower());
            }
        }

        [Fact]
        public async void TestMultipleTables()
        {
            DatabaseConnection database = new(nameof(TestMultipleTables));

            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();

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

            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();

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