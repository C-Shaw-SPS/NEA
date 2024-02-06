using MusicOrganisationApp.Lib;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Tests.Databases
{
    public class TableTests
    {
        [Fact]
        public async Task TestCaregiverSql()
        {
            await TestType(ExpectedTables.CaregiverData);
        }

        [Fact]
        public async Task TestCaregiverMapSql()
        {
            await TestType(ExpectedTables.CaregiverMaps);
        }

        [Fact]
        public async Task TestComposerSql()
        {
            await TestType(ExpectedTables.ComposerData);
        }

        [Fact]
        public async Task TestLessonSql()
        {
            await TestType(ExpectedTables.LessonData);
        }

        [Fact]
        public async Task TestLessonTimeSql()
        {
            await TestType(ExpectedTables.LessonSlotData);
        }

        [Fact]
        public async Task TestWorkSql()
        {
            await TestType(ExpectedTables.WorkData);
        }

        [Fact]
        public async Task TestRepertoireSql()
        {
            await TestType(ExpectedTables.RepertoireData);
        }

        [Fact]
        public async Task TestPupilSql()
        {
            await TestType(ExpectedTables.Pupils);
        }

        private static async Task TestType<T>(IEnumerable<T> expectedItems) where T : class, ITable, IEquatable<T>, new()
        {
            DatabaseConnection database = new(nameof(TableTests));
            await database.ResetTableAsync(expectedItems);
            IEnumerable<T> actualItems = await database.GetAllAsync<T>();
            CollectionAssert.Equal(expectedItems, actualItems);
        }
    }
}