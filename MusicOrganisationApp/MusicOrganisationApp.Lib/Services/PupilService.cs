using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilService : ISearchService<Pupil>
    {
        private readonly DatabaseConnection _database;

        public PupilService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task DeleteAsync(Pupil value)
        {
            await _database.DeleteAsync(value);
            await DeletePupilData<RepertoireData>(value.Id);
            await DeletePupilData<CaregiverMap>(value.Id);
            await DeletePupilData<LessonData>(value.Id);
            await DeletePupilData<PupilAvaliability>(value.Id);
        }


        private async Task DeletePupilData<T>(int pupilId) where T: class, IPupilIdentifiable, ITable, new()
        {
            DeleteStatement<T> deleteStatement = new();
            deleteStatement.AddCondition(nameof(IPupilIdentifiable.PupilId), pupilId);
            await _database.ExecuteAsync(deleteStatement);
        }

        public async Task<IEnumerable<Pupil>> GetAllAsync()
        {
            IEnumerable<Pupil> pupils = await _database.GetAllAsync<Pupil>();
            return pupils;
        }

        public async Task InsertAsync(Pupil value, bool getNewId)
        {
            if (getNewId)
            {
                int newId = await _database.GetNextIdAsync<Pupil>();
                value.Id = newId;
            }
            await _database.InsertAsync(value);
        }

        public async Task<IEnumerable<Pupil>> SearchAsync(string search, string ordering)
        {
            SqlQuery<Pupil> sqlQuery = new(IService.DEFAULT_LIMIT) { SelectAll = true };
            sqlQuery.AddWhereLike<Pupil>(nameof(Pupil.Name), search);
            sqlQuery.AddOrderByAscending(ordering);
            IEnumerable<Pupil> result = await _database.QueryAsync<Pupil>(sqlQuery);
            return result;
        }

        public async Task<(bool, Pupil)> TryGetAsync(int id)
        {
            (bool suceeded, Pupil pupil) result = await _database.TryGetAsync<Pupil>(id);
            return result;
        }

        public async Task UpdateAsync(Pupil value)
        {
            await _database.UpdateAsync(value);
        }
    }
}