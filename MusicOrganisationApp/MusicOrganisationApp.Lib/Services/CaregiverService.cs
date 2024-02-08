using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class CaregiverService : ISearchService<CaregiverData>
    {
        private readonly DatabaseConnection _database;

        public CaregiverService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task DeleteAsync(CaregiverData value)
        {
            await _database.DeleteAsync(value);
            DeleteStatement<CaregiverMap> deleteCaregiverMapStatement = new();
            deleteCaregiverMapStatement.AddWhereEqual(nameof(CaregiverMap.CaregiverId), value.Id);
            await _database.ExecuteAsync(deleteCaregiverMapStatement);
        }

        public async Task<IEnumerable<CaregiverData>> GetAllAsync()
        {
            IEnumerable<CaregiverData> caregivers = await _database.GetAllAsync<CaregiverData>();
            return caregivers;
        }

        public async Task InsertAsync(CaregiverData value, bool getNewId)
        {
            if (getNewId)
            {
                value.Id = await _database.GetNextIdAsync<CaregiverData>();
            }
            await _database.InsertAsync(value);
        }

        public async Task<IEnumerable<CaregiverData>> SearchAsync(string search, string ordering)
        {
            SqlQuery<CaregiverData> sqlQuery = new(IService.DEFAULT_LIMIT) { SelectAll = true };
            sqlQuery.AddWhereLike<CaregiverData>(nameof(CaregiverData.Name), search);
            sqlQuery.AddOrderByAscending(ordering);
            IEnumerable<CaregiverData> caregivers = await _database.QueryAsync<CaregiverData>(sqlQuery);
            return caregivers;
        }

        public async Task<(bool, CaregiverData)> TryGetAsync(int id)
        {
            (bool suceeded, CaregiverData caregiver) result = await _database.TryGetAsync<CaregiverData>(id);
            return result;
        }

        public async Task UpdateAsync(CaregiverData value)
        {
            await _database.UpdateAsync(value);
        }
    }
}