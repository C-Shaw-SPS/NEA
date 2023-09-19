using OpenOpusDatabase.Lib.Databases;
using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Tests
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
            Database<Composer> database = new(nameof(TestClearAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Composers);
            await database.ClearAsync();

            List<Composer> actualComposers = await database.GetAllAsync();
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
            Database<Composer> database = new(nameof(TestGetAsnyc));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Composers);
            Composer actualComposer = await database.GetAsync(Expected.Composers[0].Id);
            Assert.Equal(Expected.Composers[0], actualComposer);
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
            Database<Composer> database = new(nameof(TestUpdateAsync));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Composers);
            Composer updatedComposer = new()
            {
                Id = 36,
                Name = "Not Vaughan Williams",
                CompleteName = "Not Ralph Vaughan Williams",
                BirthDate = DateTime.Parse("1872-01-01"),
                DeathDate = DateTime.Parse("1958-01-01"),
                Era = "Not Late Romantic",
                PortraitLink = "https://assets.openopus.org/portraits/72161419-1568084957.jpg"
            };
            await database.UpdateAsync(updatedComposer);
            List<Composer> actualComposers = await database.GetAllAsync();
            Assert.Equal(updatedComposer, actualComposers[0]);
            Assert.DoesNotContain(Expected.Composers[0], actualComposers);
            for (int i = 1; i < Expected.Composers.Count; ++i)
            {
                Assert.Contains(Expected.Composers[i], actualComposers);
            }
        }
    }
}