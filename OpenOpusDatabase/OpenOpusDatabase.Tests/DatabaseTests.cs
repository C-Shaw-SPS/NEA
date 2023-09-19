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
        public async void TestInsertAllAndGetAll()
        {
            Database<Composer> database = new(nameof(TestInsertAllAndGetAll));
            await database.InsertAllAsync(Expected.Composers);
            List<Composer> actualComposers = await database.GetAllAsync();
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}