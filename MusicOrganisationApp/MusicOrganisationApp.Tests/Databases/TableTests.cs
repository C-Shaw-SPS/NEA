using MusicOrganisationApp.Lib;
using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Tests.Databases
{
    public class TableTests
    {
        [Fact]
        public async Task TestCaregiverSql()
        {
            await TestTable(ExpectedTables.CaregiverData);
        }

        [Fact]
        public async Task TestCaregiverMapSql()
        {
            await TestTable(ExpectedTables.CaregiverMaps);
        }

        [Fact]
        public async Task TestComposerSql()
        {
            await TestTable(ExpectedTables.Composers);
        }

        [Fact]
        public async Task TestLessonSql()
        {
            await TestTable(ExpectedTables.LessonData);
        }

        [Fact]
        public async Task TestLessonTimeSql()
        {
            await TestTable(ExpectedTables.LessonSlotData);
        }

        [Fact]
        public async Task TestWorkSql()
        {
            await TestTable(ExpectedTables.WorkData);
        }

        [Fact]
        public async Task TestRepertoireSql()
        {
            await TestTable(ExpectedTables.RepertoireData);
        }

        [Fact]
        public async Task TestPupilSql()
        {
            await TestTable(ExpectedTables.Pupils);
        }

        [Fact]
        public async Task TestPupilAvailabilitySql()
        {
            await TestTable(ExpectedTables.PupilAvaliabilities);
        }

        private static async Task TestTable<T>(IEnumerable<T> expectedItems) where T : class, ITable, IEquatable<T>, new()
        {
            DatabaseConnection database = new(nameof(TableTests));
            await database.ResetTableAsync(expectedItems, false);
            IEnumerable<T> actualItems = await database.GetAllAsync<T>();
            CollectionAssert.EqualContents(expectedItems, actualItems);
        }
    }
}