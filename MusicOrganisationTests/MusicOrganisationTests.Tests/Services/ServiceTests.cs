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
            await table.ClearTableAsync<Composer>();
            await table.InsertAsync(Expected.Composers[0]);
            IEnumerable<Composer> actualComposers = await table.GetAllAsync<Composer>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            Service table = new(nameof(TestClearDataAsync));
            await table.ClearTableAsync<Work>();
            await table.InsertAllAsync(Expected.Works);
            await table.ClearTableAsync<Work>();

            IEnumerable<Work> actualComposers = await table.GetAllAsync<Work>();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            Service table = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await table.ClearTableAsync<Composer>();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<Composer> actualComposers = await table.GetAllAsync<Composer>();

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public async Task TestGetAsnyc()
        {
            Service table = new(nameof(TestGetAsnyc));
            await table.ClearTableAsync<Work>();
            await table.InsertAllAsync(Expected.Works);
            Work actualWork = await table.GetAsync<Work>(Expected.Works[0].Id);
            Assert.Equal(Expected.Works[0], actualWork);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            Service table = new(nameof(TestDeleteAsync));
            await table.ClearTableAsync<Composer>();
            await table.InsertAllAsync(Expected.Composers);
            await table.DeleteAsync(Expected.Composers[0]);

            IEnumerable<Composer> actualComposers = await table.GetAllAsync<Composer>();
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
            await table.ClearTableAsync<Work>();
            await table.InsertAllAsync(Expected.Works);
            Work updatedWork = new()
            {
                Id = Expected.Works[0].Id,
                ComposerId = Expected.Works[0].ComposerId,
                Title = "Something different",
                Subtitle = "New subtitle",
                Genre = "Different genre"
            };
            await table.UpdateAsync(updatedWork);
            IEnumerable<Work> actualWorks = await table.GetAllAsync<Work>();
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
            await table.ClearTableAsync<Composer>();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<int> actualIds = await table.GetIdsAsync<Composer>();
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer.Id, actualIds);
            }
        }

        [Fact]
        public async Task TestNullProperties()
        {
            Service table = new(nameof(TestNullProperties));
            await table.ClearTableAsync<Composer>();
            await table.InsertAsync(Expected.NullPropertyComposer);
            IEnumerable<Composer> actualComposers = await table.GetAllAsync<Composer>();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            Service table = new(nameof(TestGetNextIdAsync));
            await table.ClearTableAsync<Work>();
            await table.InsertAllAsync(Expected.Works);
            int nextId = await table.GetNextIdAsync<Work>();
            Assert.Equal(Expected.Works.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async Task TestGetWhereEqualAsync()
        {
            Service table = new(nameof(TestGetWhereEqualAsync));
            await table.ClearTableAsync<Composer>();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<Composer> actualComposers = await table.GetWhereEqualAsync<Composer>(nameof(Composer.Name), Expected.Composers[0].Name);
            Assert.Contains(Expected.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestGetWhereTextLikeAsync()
        {
            Service table = new(nameof(TestGetWhereTextLikeAsync));
            await table.ClearTableAsync<Composer>();
            await table.InsertAllAsync(Expected.Composers);
            string expectedText = "ch";
            IEnumerable<Composer> actualComposers = await table.GetWhereTextLikeAsync<Composer>(nameof(Composer.Name), expectedText);
            foreach (Composer composer in actualComposers)
            {
                Assert.Contains(expectedText, composer.Name.ToLower());
            }
        }

        [Fact]
        public async void TestMultipleTables()
        {
            Service service = new(nameof(TestMultipleTables));

            await service.ClearTableAsync<Composer>();
            await service.ClearTableAsync<Work>();

            await service.InsertAllAsync(Expected.Composers);
            await service.InsertAllAsync(Expected.Works);

            IEnumerable<Composer> actualComposers = await service.GetAllAsync<Composer>();
            IEnumerable<Work> actualWorks = await service.GetAllAsync<Work>();

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
            Service service = new(nameof(TestClearOneTable));

            await service.ClearTableAsync<Composer>();
            await service.ClearTableAsync<Work>();

            await service.InsertAllAsync(Expected.Composers);
            await service.InsertAllAsync(Expected.Works);

            await service.ClearTableAsync<Composer>();

            IEnumerable<Composer> actualComposers = await service.GetAllAsync<Composer>();
            IEnumerable<Work> actualWorks = await service.GetAllAsync<Work>();

            Assert.Empty(actualComposers);

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}