using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Services
{
    public class LessonSlotServiceTests
    {
        private readonly List<LessonSlotData> _lessonSlotData = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 0,
                StartTime = new TimeSpan(09,00,00),
                EndTime = new TimeSpan(10,00,00),
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 2,
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(10,30,00)
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 3,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(12,00,00)
            },
            new LessonSlotData
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Wednesday,
                FlagIndex = 0,
                StartTime = new TimeSpan(09,30,00),
                EndTime = new TimeSpan(10,00,00)
            },
            new LessonSlotData
            {
                Id = 4,
                DayOfWeek = DayOfWeek.Wednesday,
                FlagIndex = 1,
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(10,30,00)
            },
            new LessonSlotData
            {
                Id = 5,
                DayOfWeek = DayOfWeek.Wednesday,
                FlagIndex = 3,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(12,00,00)
            },
            new LessonSlotData
            {
                Id = 6,
                DayOfWeek = DayOfWeek.Tuesday,
                FlagIndex = 1,
                StartTime = new TimeSpan(09, 00, 00),
                EndTime = new TimeSpan(10, 00, 00)
            }
        };

        [Theory]
        [InlineData(DayOfWeek.Monday, 1)]
        [InlineData(DayOfWeek.Wednesday, 2)]
        [InlineData(DayOfWeek.Tuesday, 0)]
        public async Task TestGetNextFlagIndex(DayOfWeek dayOfWeek, int expectedFlagIndex)
        {
            LessonSlotService service = new(nameof(TestGetNextFlagIndex));
            await service.ClearTableAsync<LessonSlotData>();
            await service.InsertAllAsync(_lessonSlotData);
            int actualFlagIndex = await service.GetNextFlagIndexAsync(dayOfWeek);
            Assert.Equal(expectedFlagIndex, actualFlagIndex);
        }
    }
}