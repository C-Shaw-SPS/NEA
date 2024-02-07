using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonSlotService : LessonServiceBase<LessonSlotData, LessonSlotData>
    {
        private DayOfWeek _dayOfWeek = DayOfWeek.Monday;

        public LessonSlotService(DatabaseConnection database) : base(database) { }

        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }

        public static List<DayOfWeek> GetDaysOfWeek()
        {
            List<DayOfWeek> daysOfWeek = [];
            for (DayOfWeek dayOfWeek = DayOfWeek.Monday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
            {
                daysOfWeek.Add(dayOfWeek);
            }
            daysOfWeek.Add(DayOfWeek.Sunday);
            return daysOfWeek;
        }

        protected override SqlQuery<LessonSlotData> GetAllSqlQuery()
        {
            SqlQuery<LessonSlotData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<LessonSlotData>(nameof(LessonSlotData.DayOfWeek), _dayOfWeek);
            sqlQuery.AddOrderBy(nameof(LessonSlotData.StartTime));
            return sqlQuery;
        }

        public override async Task<IEnumerable<LessonSlotData>> GetClashingLessonsAsync(object dayOfWeek, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            IEnumerable<LessonSlotData> clashingLessonSlots = await GetClashingLessonsAsync(nameof(LessonSlotData.DayOfWeek), dayOfWeek, startTime, endTime, id);
            return clashingLessonSlots;
        }

        protected override LessonSlotData GetTableValue(LessonSlotData value)
        {
            return value;
        }

        public override async Task<(bool, LessonSlotData)> TryGetAsync(int id)
        {
            (bool suceeded, LessonSlotData value) result = await _database.TryGetAsync<LessonSlotData>(id);
            return result;
        }

        protected override SqlQuery<LessonSlotData> GetSqlQueryWithNoConditions()
        {
            SqlQuery<LessonSlotData> sqlQuery = new() { SelectAll = true };
            return sqlQuery;
        }
    }
}