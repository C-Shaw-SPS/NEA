using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonSlotService : IService<LessonSlotData>
    {
        private readonly DatabaseConnection _database;
        private DayOfWeek _dayOfWeek;

        public LessonSlotService(DatabaseConnection database)
        {
            _database = database;
        }

        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }

        public static List<DayOfWeek> GetDaysOfWeek()
        {
            List<DayOfWeek> daysOfWeek = [];
            for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Monday; dayOfWeek++)
            {
                daysOfWeek.Add(dayOfWeek);
            }
            return daysOfWeek;
        }

        public async Task DeleteAsync(LessonSlotData value)
        {
            await _database.DeleteAsync(value);
        }

        public async Task<IEnumerable<LessonSlotData>> GetAllAsync()
        {
            SqlQuery<LessonSlotData> sqlQuery = GetSqlQuery();
            IEnumerable<LessonSlotData> lessonSlots = await _database.QueryAsync<LessonSlotData>(sqlQuery);
            return lessonSlots;
        }

        private SqlQuery<LessonSlotData> GetSqlQuery()
        {
            SqlQuery<LessonSlotData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<LessonSlotData>(nameof(LessonSlotData.DayOfWeek), _dayOfWeek);
            return sqlQuery;
        }

        public async Task InsertAsync(LessonSlotData value, bool getNewId)
        {
            if (getNewId)
            {
                int id = await _database.GetNextIdAsync<LessonSlotData>();
                value.Id = id;
            }
            await _database.InsertAsync(value);
        }

        public Task<(bool, LessonSlotData)> TryGetAsync(int id)
        {
            return _database.TryGetAsync<LessonSlotData>(id);
        }

        public async Task UpdateAsync(LessonSlotData value)
        {
            await _database.UpdateAsync(value);
        }

        public async Task<IEnumerable<LessonSlotData>> GetClashingLessonSlots(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            SqlQuery<LessonSlotData> sqlQuery = GetClashSqlQuery(dayOfWeek, startTime, endTime);
            IEnumerable<LessonSlotData> clashingLessonSlots = await _database.QueryAsync<LessonSlotData>(sqlQuery);
            return clashingLessonSlots;
        }

        private static SqlQuery<LessonSlotData> GetClashSqlQuery(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            SqlQuery<LessonSlotData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<LessonSlotData>(nameof(LessonSlotData.DayOfWeek), dayOfWeek);
            sqlQuery.AddAndLessOrEqual<LessonSlotData>(nameof(LessonSlotData.StartTime), startTime);
            sqlQuery.AddAndGreaterThan<LessonSlotData>(nameof(LessonSlotData.EndTime), startTime);
            sqlQuery.AddOrEqual<LessonSlotData>(nameof(LessonSlotData.DayOfWeek), dayOfWeek);
            sqlQuery.AddAndGreaterOrEqual<LessonSlotData>(nameof(LessonSlotData.StartTime), startTime);
            sqlQuery.AddAndLessThan<LessonSlotData>(nameof(LessonSlotData.StartTime), endTime);
            return sqlQuery;
        }
    }
}