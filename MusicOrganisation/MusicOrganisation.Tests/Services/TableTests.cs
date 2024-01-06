using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Services;

namespace MusicOrganisation.Tests.Services
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
            await TestType(Expected.CaregiverMap);
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
            Service table = new(nameof(TableTests));
            await table.ClearTableAsync<T>();
            await table.InsertAllAsync(expectedItems);
            IEnumerable<T> actual = await table.GetAllAsync<T>();
            Assert.Equal(expectedItems.Count(), actual.Count());
            foreach (T expectedItem in expectedItems)
            {
                Assert.Contains(expectedItem, actual);
            }
        }

        private static async Task TestType<T>(params T[] expectedItems) where T : class, ITable, new()
        {
            await TestType((IEnumerable<T>)expectedItems);
        }
    }
}