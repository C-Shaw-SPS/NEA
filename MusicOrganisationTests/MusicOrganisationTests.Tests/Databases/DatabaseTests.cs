using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Databases;

namespace MusicOrganisationTests.Tests.Databases
{
    public class DatabaseTests
    {
        [Fact]
        public async Task TestInsertAsync()
        {
            TableConnection<Composer> table = new(nameof(TestInsertAsync));
            await table.ClearDataAsync();
            await table.InsertAsync(Expected.Composers[0]);
            IEnumerable<Composer> actualComposers = await table.GetAllAsync();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestClearDataAsync()
        {
            TableConnection<Work> table = new(nameof(TestClearDataAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Works);
            await table.ClearDataAsync();

            IEnumerable<Work> actualComposers = await table.GetAllAsync();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async Task TestInsertAllAsyncAndGetAllAsync()
        {
            TableConnection<Composer> table = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<Composer> actualComposers = await table.GetAllAsync();

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public async Task TestGetAsnyc()
        {
            TableConnection<Work> table = new(nameof(TestGetAsnyc));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Works);
            Work actualWork = await table.GetAsync(Expected.Works[0].Id);
            Assert.Equal(Expected.Works[0], actualWork);
        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            TableConnection<Composer> table = new(nameof(TestDeleteAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Composers);
            await table.DeleteAsync(Expected.Composers[0]);

            IEnumerable<Composer> actualComposers = await table.GetAllAsync();
            Assert.DoesNotContain(Expected.Composers[0], actualComposers);
            for (int i = 1; i < Expected.Composers.Count; ++i)
            {
                Assert.Contains(Expected.Composers[i], actualComposers);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            TableConnection<Work> table = new(nameof(TestUpdateAsync));
            await table.ClearDataAsync();
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
            IEnumerable<Work> actualWorks = await table.GetAllAsync();
            Assert.DoesNotContain(Expected.Works[0], actualWorks);
            for (int i = 1; i < Expected.Works.Count; ++i)
            {
                Assert.Contains(Expected.Works[i], actualWorks);
            }
        }

        [Fact]
        public async Task TestGetIdsAsync()
        {
            TableConnection<Composer> table = new(nameof(TestGetIdsAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<int> actualIds = await table.GetIdsAsync();
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer.Id, actualIds);
            }
        }

        [Fact]
        public async Task TestNullProperties()
        {
            TableConnection<Composer> table = new(nameof(TestNullProperties));
            await table.ClearDataAsync();
            await table.InsertAsync(Expected.NullPropertyComposer);
            IEnumerable<Composer> actualComposers = await table.GetAllAsync();
            Assert.Single(actualComposers);
            Assert.Contains(Expected.NullPropertyComposer, actualComposers);
        }

        [Fact]
        public async Task TestGetNextIdAsync()
        {
            TableConnection<Work> table = new(nameof(TestGetNextIdAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Works);
            int nextId = await table.GetNextIdAsync();
            Assert.Equal(Expected.Works.Max(w => w.Id) + 1, nextId);
        }

        [Fact]
        public async Task TestGetWhereEqualAsync()
        {
            TableConnection<Composer> table = new(nameof(TestGetWhereEqualAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Composers);
            IEnumerable<Composer> actualComposers = await table.GetWhereEqualAsync(nameof(Composer.Name), Expected.Composers[0].Name);
            Assert.Contains(Expected.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestGetWhereTextLikeAsync()
        {
            TableConnection<Composer> table = new(nameof(TestGetWhereTextLikeAsync));
            await table.ClearDataAsync();
            await table.InsertAllAsync(Expected.Composers);
            string expectedText = "ch";
            IEnumerable<Composer> actualComposers = await table.GetWhereTextLikeAsync(nameof(Composer.Name), expectedText);
            foreach (Composer composer in actualComposers)
            {
                Assert.Contains(expectedText, composer.Name.ToLower());
            }
        }
    }
}