using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class LessonSlotServiceTests
    {
        [Fact]
        public async Task TestGetClashingLessonSlotsAsync()
        {
            (DatabaseConnection database, LessonSlotService service) = await GetDatabaseAndService(nameof(TestGetClashingLessonSlotsAsync), true);
            DayOfWeek dayOfWeek = ExpectedService.NewLessonSlot.DayOfWeek;
            TimeSpan startTime = ExpectedService.NewLessonSlot.StartTime;
            TimeSpan endTime = ExpectedService.NewLessonSlot.EndTime;
            IEnumerable<LessonSlot> actualClashingLessonSlots = await service.GetClashingLessonsAsync(dayOfWeek, startTime, endTime, null);
            CollectionAssert.Equal(ExpectedService.ClashingLessonSlots, actualClashingLessonSlots);
        }

        private static async Task<(DatabaseConnection database, LessonSlotService service)> GetDatabaseAndService(string path, bool addLessonSlots)
        {
            DatabaseConnection database = new(path);
            await database.DropTableIfExistsAsync<LessonSlot>();
            if (addLessonSlots)
            {
                await database.InsertAllAsync(ExpectedService.LessonSlots);
            }
            LessonSlotService service = new(database);
            return (database, service);
        }
    }
}