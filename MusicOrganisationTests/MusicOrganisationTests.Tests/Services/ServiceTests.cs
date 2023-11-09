using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Services
{
    public class ServiceTests
    {
        [Fact]
        public async Task TestInsertAsync()
        {
            Service table = new(nameof(TestInsertAsync));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAsync(Expected.Composers[0]);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            Service table = new(nameof(TestClearDataAsync));
            await table.ClearTableAsync<WorkData>();
            await table.InsertAllAsync(Expected.Works);
            await table.ClearTableAsync<WorkData>();

            IEnumerable<WorkData> actualComposers = await table.GetAllAsync<WorkData>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            Service table = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());
            foreach (ComposerData expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public async Task TestGetAsnyc()
        {
            Service table = new(nameof(TestGetAsnyc));
            await table.ClearTableAsync<WorkData>();
            await table.InsertAllAsync(Expected.Works);
            WorkData actualWork = await table.GetAsync<WorkData>(Expected.Works[0].Id);
            Assert.Equal(Expected.Works[0], actualWork);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            Service table = new(nameof(TestDeleteAsync));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAllAsync(Expected.Composers);
            await table.DeleteAsync(Expected.Composers[0]);

            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.DoesNotContain(Expected.Composers[0], actualComposers);
            for (int i = 1; i < Expected.Composers.Count; ++i)
            {
                Assert.Contains(Expected.Composers[i], actualComposers);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            Service table = new(nameof(TestUpdateAsync));
            await table.ClearTableAsync<WorkData>();
            await table.InsertAllAsync(Expected.Works);
            WorkData updatedWork = new()
            {
                Id = Expected.Works[0].Id,
                ComposerId = Expected.Works[0].ComposerId,
                Title = "Something different",
                Subtitle = "New subtitle",
                Genre = "Different genre"
            };
            await table.UpdateAsync(updatedWork);
            IEnumerable<WorkData> actualWorks = await table.GetAllAsync<WorkData>();
            Assert.DoesNotContain(Expected.Works[0], actualWorks);
            for (int i = 1; i < Expected.Works.Count; ++i)
            {
                Assert.Contains(Expected.Works[i], actualWorks);
            }
        }

        [Fact]
        public async Task TestGetIdsAsync()
        {
            Service table = new(nameof(TestGetIdsAsync));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<int> actualIds = await table.GetIdsAsync<ComposerData>();
            foreach (ComposerData expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer.Id, actualIds);
            }
        }

        [Fact]
        public async Task TestNullProperties()
        {
            Service table = new(nameof(TestNullProperties));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAsync(Expected.NullPropertyComposer);
            IEnumerable<ComposerData> actualComposers = await table.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            Service table = new(nameof(TestGetNextIdAsync));
            await table.ClearTableAsync<WorkData>();
            await table.InsertAllAsync(Expected.Works);
            int nextId = await table.GetNextIdAsync<WorkData>();
            Assert.Equal(Expected.Works.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async Task TestGetWhereEqualAsync()
        {
            Service table = new(nameof(TestGetWhereEqualAsync));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<ComposerData> actualComposers = await table.GetWhereEqualAsync<ComposerData>(nameof(ComposerData.Name), Expected.Composers[0].Name);
            Assert.Contains(Expected.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestGetWhereTextLikeAsync()
        {
            Service table = new(nameof(TestGetWhereTextLikeAsync));
            await table.ClearTableAsync<ComposerData>();
            await table.InsertAllAsync(Expected.Composers);
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

            await service.ClearTableAsync<ComposerData>();
            await service.ClearTableAsync<WorkData>();

            await service.InsertAllAsync(Expected.Composers);
            await service.InsertAllAsync(Expected.Works);

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await service.GetAllAsync<WorkData>();

            foreach (ComposerData expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }

            foreach (WorkData expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async void TestClearOneTable()
        {
            Service service = new(nameof(TestClearOneTable));

            await service.ClearTableAsync<ComposerData>();
            await service.ClearTableAsync<WorkData>();

            await service.InsertAllAsync(Expected.Composers);
            await service.InsertAllAsync(Expected.Works);

            await service.ClearTableAsync<ComposerData>();

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> actualWorks = await service.GetAllAsync<WorkData>();

            Assert.Empty(actualComposers);

            foreach (WorkData expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}