using OpenOpusDatabase.Lib.Databases;
using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Tests.Databases
{
    public class DatabaseTests
    {
        [Fact]
        public async void TestInsertAsync()
        {
            Database<Composer> database = new(nameof(TestInsertAsync));
            await database.ClearAsync();
            await database.InsertAsync(Expected.Composers[0]);
            List<Composer> actualComposers = await database.GetAllAsync();
            Assert.Single(actualComposers);
            Assert.Equal(Expected.Composers[0], actualComposers[0]);
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
            Database<Composer> database = new(nameof(TestInsertAllAsyncAndGetAllAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Composers);
            List<Composer> actualComposers = await database.GetAllAsync();

            Assert.Equal(Expected.Composers.Count, actualComposers.Count);
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
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
            Database<Composer> database = new(nameof(TestDeleteAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Composers);
            await database.DeleteAsync(Expected.Composers[0]);

            List<Composer> actualComposers = await database.GetAllAsync();
            Assert.DoesNotContain(Expected.Composers[0], actualComposers);
            for (int i = 1; i < Expected.Composers.Count; ++i)
            {
                Assert.Contains(Expected.Composers[i], actualComposers);
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
