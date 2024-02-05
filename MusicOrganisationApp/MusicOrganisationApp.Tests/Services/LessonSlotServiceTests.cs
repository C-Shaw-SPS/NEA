using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IEnumerable<LessonSlotData> actualClashingLessonSlots = await service.GetClashingLessonSlots(dayOfWeek, startTime, endTime);
            CollectionAssert.Equal(ExpectedService.ClashingLessonSlots, actualClashingLessonSlots);
        }

        private static async Task<(DatabaseConnection database, LessonSlotService service)> GetDatabaseAndService(string path, bool addLessonSlots)
        {
            DatabaseConnection database = new(path);
            await database.DropTableIfExistsAsync<LessonSlotData>();
            if (addLessonSlots)
            {
                await database.InsertAllAsync(ExpectedService.LessonSlots);
            }
            LessonSlotService service = new(database);
            return (database, service);
        }
    }
}