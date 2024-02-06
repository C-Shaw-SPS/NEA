using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonSlotService : IService<LessonSlotData>
    {
        private readonly DatabaseConnection _database;
        private DayOfWeek _dayOfWeek = DayOfWeek.Monday;

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
            for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
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
            sqlQuery.AddOrderBy(nameof(LessonSlotData.StartTime));
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

        public async Task<IEnumerable<LessonSlotData>> GetClashingLessonSlots(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            IEnumerable<LessonSlotData> clashingLessonSots = await ILesson.GetClashingLessons<LessonSlotData>(_database, nameof(LessonSlotData.DayOfWeek), dayOfWeek, startTime, endTime, id);
            return clashingLessonSots;
        }
    }
}