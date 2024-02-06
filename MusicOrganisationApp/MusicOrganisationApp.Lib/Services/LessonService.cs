using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonService : IService<LessonData>
    {
        private readonly DatabaseConnection _database;
        private DateTime _date = DateTime.Today;

        public LessonService(DatabaseConnection database)
        {
            _database = database;
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value.Date;
        }

        public async Task DeleteAsync(LessonData value)
        {
            await _database.DeleteAsync(value);
        }

        public async Task<IEnumerable<LessonData>> GetAllAsync()
        {
            SqlQuery<LessonData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<LessonData>(nameof(LessonData.Date), _date);
            IEnumerable<LessonData> lessons = await _database.QueryAsync<LessonData>(sqlQuery);
            return lessons;
        }

        public async Task InsertAsync(LessonData value, bool getNewId)
        {
            if (getNewId)
            {
                int id = await _database.GetNextIdAsync<LessonData>();
                value.Id = id;
            }
            await _database.InsertAsync(value);
        }

        public async Task<(bool, LessonData)> TryGetAsync(int id)
        {
            (bool suceeded, LessonData value) result = await _database.TryGetAsync<LessonData>(id);
            return result;
        }

        public async Task UpdateAsync(LessonData value)
        {
            await _database.UpdateAsync(value);
        }
    }
}