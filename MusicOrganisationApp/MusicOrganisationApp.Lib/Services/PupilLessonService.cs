using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilLessonService : IService<LessonData>
    {
        private readonly DatabaseConnection _database;
        private int? _pupilId;

        public PupilLessonService(DatabaseConnection database)
        {
            _database = database;
        }

        public int? PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public async Task DeleteAsync(LessonData value)
        {
            await _database.DeleteAsync(value);
        }

        public async Task<IEnumerable<LessonData>> GetAllAsync()
        {
            SqlQuery<LessonData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEqual<LessonData>(nameof(LessonData.PupilId), _pupilId);
            sqlQuery.AddOrderByDescending(nameof(LessonData.Date));
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
            (bool suceeded, LessonData lesson) result = await _database.TryGetAsync<LessonData>(id);
            return result;
        }

        public async Task UpdateAsync(LessonData value)
        {
            await _database.UpdateAsync(value);
        }
    }
}