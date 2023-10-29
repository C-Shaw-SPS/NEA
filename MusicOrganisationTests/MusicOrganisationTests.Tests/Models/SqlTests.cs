using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Models
{
    public class SqlTests
    {
        [Fact]
        public async Task TestComposerSql()
        {
            await TestType(Expected.Composers, nameof(TestComposerSql));
        }

        [Fact]
        public async Task TestWorkSql()
        {
            await TestType(Expected.Works, nameof(TestWorkSql));
        }

        [Fact]
        public async Task TestRepertoireSql()
        {
            await TestType(Expected.Repertoires, nameof(TestRepertoireSql));
        }

        [Fact]
        public async Task TestCaregiverSql()
        {
            await TestType(Expected.Caregivers, nameof(TestCaregiverSql));
        }

        [Fact]
        public async Task TestPupilSql()
        {
            await TestType(Expected.Pupils, nameof(TestPupilSql));
        }

        private async static Task TestType<T>(IEnumerable<T> expectedItems, string databaseName) where T : class, ISqlStorable, new()
        {
            TableConnection<T> table = new(databaseName);
            await table.ClearAsync();
            await table.InsertAllAsync(expectedItems);
            IEnumerable<T> actual = await table.GetAllAsync();
            Assert.Equal(expectedItems.Count(), actual.Count());
            foreach (T expectedItem in expectedItems)
            {
                Assert.Contains(expectedItem, actual);
            }
        }
    }
}