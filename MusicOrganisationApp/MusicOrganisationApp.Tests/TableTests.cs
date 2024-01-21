using MusicOrganisationApp.Lib;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Tests
{
    public class TableTests
    {
        [Fact]
        public async Task TestCaregiverSql()
        {
            await TestType(Expected.CaregiverData);
        }

        [Fact]
        public async Task TestCaregiverMapSql()
        {
            await TestType(Expected.CaregiverMaps);
        }

        [Fact]
        public async Task TestComposerSql()
        {
            await TestType(Expected.ComposerData);
        }

        [Fact]
        public async Task TestLessonSql()
        {
            await TestType(Expected.LessonData);
        }

        [Fact]
        public async Task TestLessonTimeSql()
        {
            await TestType(Expected.LessonSlotData);
        }

        [Fact]
        public async Task TestWorkSql()
        {
            await TestType(Expected.WorkData);
        }

        [Fact]
        public async Task TestRepertoireSql()
        {
            await TestType(Expected.RepertoireData);
        }

        [Fact]
        public async Task TestPupilSql()
        {
            await TestType(Expected.Pupils);
        }

        private static async Task TestType<T>(IEnumerable<T> expectedItems) where T : class, ITable, new()
        {
            DatabaseConnection database = new(nameof(TableTests));
            await database.DropTableAsync<T>();
            await database.InsertAllAsync(expectedItems);
            IEnumerable<T> actual = await database.GetAllAsync<T>();
            Assert.Equal(expectedItems.Count(), actual.Count());
            foreach (T expectedItem in expectedItems)
            {
                Assert.Contains(expectedItem, actual);
            }
        }
    }
}