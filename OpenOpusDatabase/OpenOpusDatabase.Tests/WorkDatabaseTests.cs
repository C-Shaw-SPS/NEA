using OpenOpusDatabase.Lib.Databases;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests
{
    public class WorkDatabaseTests
    {
        [Fact]
        public async void TestInsertAsync()
        {
            Database<Work> database = new(nameof(TestInsertAsync));
            await database.ClearAsync();
            await database.InsertAsync(Expected.Works[0]);
            List<Work> actualWorks = await database.GetAllAsync();
            Assert.Single(actualWorks);
            Assert.Equal(Expected.Works[0], actualWorks[0]);
        }

        [Fact]
        public async void TestClearAsync()
        {
            Database<Work> database = new(nameof(TestClearAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Works);
            await database.ClearAsync();

            List<Work> actualComposers = await database.GetAllAsync();
            Assert.Empty(actualComposers);
        }

        [Fact]
        public async void TestInsertAllAsyncAndGetAllAsync()
        {
            Database<Work> database = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Works);
            List<Work> actualWorks = await database.GetAllAsync();

            Assert.Equal(Expected.Works.Count, actualWorks.Count);
            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async void TestGetAsnyc()
        {
            Database<Work> database = new(nameof(TestGetAsnyc));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Works);
            Work actualWork = await database.GetAsync(Expected.Works[0].Id);
            Assert.Equal(Expected.Works[0], actualWork);
        }

        [Fact]
        public async void TestDeleteAsync()
        {
            Database<Work> database = new(nameof(TestDeleteAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Works);
            await database.DeleteAsync(Expected.Works[0]);

            List<Work> actualWorks = await database.GetAllAsync();
            Assert.DoesNotContain(Expected.Works[0], actualWorks);
            for (int i = 1; i < Expected.Works.Count; ++i)
            {
                Assert.Contains(Expected.Works[i], actualWorks);
            }
        }

        [Fact]
        public async void TestUpdateAsync()
        {
            Database<Work> database = new(nameof(TestUpdateAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Works);
            Work updatedWork = new()
            {
                Id = 1,
                ComposerId = 176,
                Title = "Not 3 Movements",
                Subtitle = "Not a subtitle",
                Genre = "Not Orchestral"
            };
            await database.UpdateAsync(updatedWork);
            List<Work> actualWorks = await database.GetAllAsync();
            Assert.Equal(updatedWork, actualWorks[0]);
            Assert.DoesNotContain(Expected.Works[0], actualWorks);
            for (int i = 1; i < Expected.Works.Count; ++i)
            {
                Assert.Contains(Expected.Works[i], actualWorks);
            }
        }
    }
}
