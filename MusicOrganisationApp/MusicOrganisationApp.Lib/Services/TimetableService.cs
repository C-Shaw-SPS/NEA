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

        public async Task<bool> TryGenerateTimetable(DateTime date)
        {
            (DateTime weekBefore, DateTime startOfWeek, DateTime endOfWeek) = GetWeekDates(date);
            await DeleteLessonsInRangeAsync(startOfWeek, endOfWeek);
            IEnumerable<Pupil> pupils = await _database.GetAllAsync<Pupil>();
            IEnumerable<PupilAvailability> pupilAvailability = await _database.GetAllAsync<PupilAvailability>();
            IEnumerable<LessonSlotData> lessonSlots = await _database.GetAllAsync<LessonSlotData>();
            IEnumerable<LessonData> prevLessons = await GetLessonsInRangeAsync(weekBefore, startOfWeek);
            TimetableGenerator timetableGenerator = new(pupils, pupilAvailability, lessonSlots, prevLessons);
            bool suceeded = timetableGenerator.TryGenerateTimetable(out Dictionary<int, int> timetable);
            await InsertTimetableAsync(startOfWeek, timetable, lessonSlots);
            return suceeded;
        }

        private static (DateTime weekBefore, DateTime startOfWeek, DateTime endOfWeek) GetWeekDates(DateTime date)
        {
            DateTime startOfWeek = date;
            while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
            {
                startOfWeek -= _DAY;
            }
            DateTime endOfWeek = startOfWeek + _WEEK;
            DateTime weekBefore = startOfWeek - _WEEK;
            return (weekBefore, startOfWeek, endOfWeek);
        }

        private async Task DeleteLessonsInRangeAsync(DateTime start, DateTime end)
        {
            DeleteStatement<LessonData> deleteStatement = GetDeleteLessonsStatement(start, end);
            await _database.ExecuteAsync(deleteStatement);
        }

        private static DeleteStatement<LessonData> GetDeleteLessonsStatement(DateTime start, DateTime end)
        {
            DeleteStatement<LessonData> deleteStatement = new();
            deleteStatement.AddWhereGreaterOrEqual<LessonData>(nameof(LessonData.Date), start);
            deleteStatement.AddAndLessThan<LessonData>(nameof(LessonData.Date), end);
            return deleteStatement;
        }

        private async Task<IEnumerable<LessonData>> GetLessonsInRangeAsync(DateTime start, DateTime end)
        {
            SqlQuery<LessonData> sqlQuery = GetLessonsSqlQuery(start, end);
            IEnumerable<LessonData> lessons = await _database.QueryAsync<LessonData>(sqlQuery);
            return lessons;
        }

        private static SqlQuery<LessonData> GetLessonsSqlQuery(DateTime startOfWeek, DateTime endOfWeek)
        {
            SqlQuery<LessonData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereGreaterOrEqual<LessonData>(nameof(LessonData.Date), startOfWeek);
            sqlQuery.AddAndLessThan<LessonData>(nameof(LessonData.Date), endOfWeek);
            return sqlQuery;
        }

        private async Task InsertTimetableAsync(DateTime startOfWeek, Dictionary<int, int> timetable, IEnumerable<LessonSlotData> lessonSlotsEnumerable)
        {
            Dictionary<int, LessonSlotData> lessonSlots = lessonSlotsEnumerable.GetDictionary();
            Dictionary<DayOfWeek, DateTime> daysInWeek = GetDaysInWeek(startOfWeek);
            List<LessonData> lessons = [];
            int id = await _database.GetNextIdAsync<LessonData>();
            foreach (int lessonSlotId in timetable.Keys)
            {
                LessonData lesson = new()
                {
                    Id = id,
                    PupilId = timetable[lessonSlotId],
                    Date = daysInWeek[lessonSlots[lessonSlotId].DayOfWeek],
                    StartTime = lessonSlots[lessonSlotId].StartTime,
                    EndTime = lessonSlots[lessonSlotId].EndTime
                };
                lessons.Add(lesson);
                ++id;
            }
            await _database.InsertAllAsync(lessons);
        }

        private static Dictionary<DayOfWeek, DateTime> GetDaysInWeek(DateTime startOfWeek)
        {
            Dictionary<DayOfWeek, DateTime> daysInWeek = new();
            DateTime dayInWeek = startOfWeek;
            do
            {
                daysInWeek.Add(dayInWeek.DayOfWeek, dayInWeek);
            } while (dayInWeek.DayOfWeek != startOfWeek.DayOfWeek);
            return daysInWeek;
        }
    }
}