using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class TimetableService
    {
        private static readonly TimeSpan _DAY = TimeSpan.FromDays(1);
        private static readonly TimeSpan _WEEK = TimeSpan.FromDays(7);

        private readonly DatabaseConnection _database;

        public TimetableService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task GenerateTimetable(DateTime date)
        {

        }

        private async Task DeleteLessonsInWeek(DateTime date)
        {
            (DateTime startOfWeek, DateTime endOfWeek) = GetStartAndEndOfWeek(date);

        }

        private static (DateTime startOfWeek, DateTime endOfWeek) GetStartAndEndOfWeek(DateTime date)
        {
            DateTime startOfWeek = date;
            while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                startOfWeek -= _DAY;
            }
            DateTime endOfWeek = startOfWeek + _WEEK;
            return (startOfWeek, endOfWeek);
        }

        private static DeleteStatement<LessonData> GetDeleteLessonsStatement(DateTime startOfWeek, DateTime endOfWeek)
        {
            DeleteStatement<LessonData> deleteStatement = new();
            throw new NotImplementedException();
        }
    }
}