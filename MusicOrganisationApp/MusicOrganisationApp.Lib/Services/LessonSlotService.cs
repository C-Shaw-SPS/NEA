using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonSlotService : IService<LessonSlotData>
    {
        private readonly DatabaseConnection _database;
        private DayOfWeek _dayOfWeek;

        public LessonSlotService(DatabaseConnection database, DayOfWeek dayOfWeek)
        {
            _database = database;
            _dayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
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

        public async Task<IEnumerable<LessonSlotData>> GetClashingLessonSlots(LessonSlotData value)
        {
            string sqlQuery = GetClashSqlQuery(value);
            await _database.CreateTableAsync<LessonSlotData>();
            IEnumerable<LessonSlotData> clashingLessonSlots = await _database.QueryAsync<LessonSlotData>(sqlQuery);
            return clashingLessonSlots;
        }

        private static string GetClashSqlQuery(LessonSlotData value)
        {
            string sqlQuery = $"""
                SELECT * FROM {LessonSlotData.TableName}
                WHERE {LessonSlotData.TableName}.{nameof(LessonSlotData.DayOfWeek)} = {value.DayOfWeek}
                AND ({value.StartTime} BETWEEN {LessonSlotData.TableName}.{nameof(LessonSlotData.StartTime)} AND {LessonSlotData.TableName}.{nameof(LessonSlotData.EndTime)} - 1
                OR {LessonSlotData.TableName}.{nameof(LessonSlotData.StartTime)} BETWEEN {value.StartTime.Ticks} AND {value.EndTime.Ticks - 1})
                """;
            return sqlQuery;
        }
    }
}