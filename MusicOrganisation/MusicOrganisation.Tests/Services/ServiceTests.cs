using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Tests.Services
{
    public class ServiceTests
    {
        [Fact]
        public async Task TestEmptyQueryAsync()
        {
            Service service = new(nameof(TestEmptyQueryAsync));
            await service.QueryAsync<ComposerData>(string.Empty);
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            Service table = new(nameof(TestInsertAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAsync(Expected.ComposerData[0]);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.ComposerData[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            Service table = new(nameof(TestClearDataAsync));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            await table.ClearTableAsync<WorkData>();

            IEnumerable<WorkData> actualComposers = await table.GetAllAsync<WorkData>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            Service table = new(nameof(TestInsertAllAsyncAndGetAllAsync));
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
            Service table = new(nameof(TestGetAsnyc));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            WorkData actualWork = await table.GetAsync<WorkData>(Expected.WorkData[0].Id);
            Assert.Equal(Expected.WorkData[0], actualWork);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            Service table = new(nameof(TestDeleteAsync));
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
            Service table = new(nameof(TestUpdateAsync));
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
            Service table = new(nameof(TestGetIdsAsync));
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
            Service table = new(nameof(TestNullProperties));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAsync(Expected.NullPropertyComposer);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            Service table = new(nameof(TestGetNextIdAsync));
            await table.DropTableIfExistsAsync<WorkData>();
            await table.InsertAllAsync(Expected.WorkData);
            int nextId = await table.GetNextIdAsync<WorkData>();
            Assert.Equal(Expected.WorkData.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async Task TestGetWhereEqualAsync()
        {
            Service table = new(nameof(TestGetWhereEqualAsync));
            await table.DropTableIfExistsAsync<ComposerData>();
            await table.InsertAllAsync(Expected.ComposerData);
            IEnumerable<ComposerData> actualComposers = await table.GetWhereEqualAsync<ComposerData>(nameof(ComposerData.Name), Expected.ComposerData[0].Name);
            Assert.Contains(Expected.ComposerData[0], actualComposers);
        }

        [Fact]
        public async Task TestGetWhereTextLikeAsync()
        {
            Service table = new(nameof(TestGetWhereTextLikeAsync));
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
            Service service = new(nameof(TestMultipleTables));

            await service.DropTableIfExistsAsync<ComposerData>();
            await service.DropTableIfExistsAsync<WorkData>();

            await service.InsertAllAsync(Expected.ComposerData);
            await service.InsertAllAsync(Expected.WorkData);

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await service.GetAllAsync<WorkData>();

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
            Service service = new(nameof(TestClearOneTable));

            await service.DropTableIfExistsAsync<ComposerData>();
            await service.DropTableIfExistsAsync<WorkData>();

            await service.InsertAllAsync(Expected.ComposerData);
            await service.InsertAllAsync(Expected.WorkData);

            await service.ClearTableAsync<ComposerData>();

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await service.GetAllAsync<WorkData>();

            Assert.Empty(actualComposers);

            foreach (WorkData expectedWork in Expected.WorkData)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}