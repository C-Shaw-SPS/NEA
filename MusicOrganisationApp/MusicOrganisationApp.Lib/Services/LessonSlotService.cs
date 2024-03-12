using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonSlotService : LessonServiceBase<LessonSlot, LessonSlot>
    {
        private DayOfWeek _dayOfWeek = DayOfWeek.Sunday;

        public LessonSlotService(IDatabaseConnection database) : base(database) { }

        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }

        public static List<DayOfWeek> GetDaysOfWeek()
        {
            List<DayOfWeek> daysOfWeek = [];
            for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
            {
                daysOfWeek.Add(dayOfWeek);
            }
            return daysOfWeek;
        }

        protected override SqlQuery<LessonSlot> GetAllSqlQuery()
        {
            SqlQuery<LessonSlot> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEqual<LessonSlot>(nameof(LessonSlot.DayOfWeek), _dayOfWeek);
            sqlQuery.AddOrderByAscending(nameof(LessonSlot.StartTime));
            return sqlQuery;
        }

        public override async Task<IEnumerable<LessonSlot>> GetClashingLessonsAsync(object dayOfWeek, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            IEnumerable<LessonSlot> clashingLessonSlots = await GetClashingLessonsAsync(nameof(LessonSlot.DayOfWeek), dayOfWeek, startTime, endTime, id);
            return clashingLessonSlots;
        }

        protected override LessonSlot GetTableValue(LessonSlot value)
        {
            return value;
        }

        public override async Task<(bool, LessonSlot)> TryGetAsync(int id)
        {
            (bool succeeded, LessonSlot value) result = await _database.TryGetAsync<LessonSlot>(id);
            return result;
        }

        protected override SqlQuery<LessonSlot> GetSqlQueryWithNoConditions()
        {
            SqlQuery<LessonSlot> sqlQuery = new() { SelectAll = true };
            return sqlQuery;
        }
    }
}