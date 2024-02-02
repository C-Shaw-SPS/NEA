using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilCaregiverService : IService<Caregiver>
    {
        private readonly DatabaseConnection _database;
        private int? _pupilId;

        public PupilCaregiverService(DatabaseConnection database)
        {
            _database = database;
        }

        public int? PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public async Task DeleteAsync(Caregiver value)
        {
            await _database.DeleteAsync<CaregiverMap>(value.MapId);
        }

        public async Task<IEnumerable<Caregiver>> GetAllAsync()
        {
            SqlQuery<CaregiverData> sqlQuery = GetSqlQuery();
            IEnumerable<Caregiver> caregivers = await _database.QueryAsync<Caregiver>(sqlQuery);
            return caregivers;
        }

        public async Task InsertAsync(Caregiver value, bool getNewId)
        {
            CaregiverMap caregiverMap = await GetCaregiverMap(value, getNewId);
            await _database.InsertAsync(caregiverMap);
        }

        public async Task<IEnumerable<Caregiver>> SearchAsync(string search, string ordering)
        {
            SqlQuery<CaregiverData> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereEquals<CaregiverMap>(nameof(CaregiverMap.PupilId), _pupilId);
            sqlQuery.AddOrderBy(ordering);
            IEnumerable<Caregiver> caregivers = await _database.QueryAsync<Caregiver>(sqlQuery);
            return caregivers;
        }

        public async Task<(bool, Caregiver)> TryGetAsync(int id)
        {
            SqlQuery<CaregiverData> sqlQuery = GetSqlQuery();
            IEnumerable<Caregiver> caregivers = await _database.QueryAsync<Caregiver>(sqlQuery);
            return IService<Caregiver>.TryReturnValue(caregivers);
        }

        public async Task UpdateAsync(Caregiver value)
        {
            CaregiverMap caregiverMap = await GetCaregiverMap(value, false);
            await _database.UpdateAsync(caregiverMap);
        }

        private static SqlQuery<CaregiverData> GetSqlQuery()
        {
            SqlQuery<CaregiverData> sqlQuery = new();
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.Id), nameof(Caregiver.Id));
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.Name), nameof(Caregiver.Name));
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.Email), nameof(Caregiver.Email));
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.PhoneNumber), nameof(Caregiver.PhoneNumber));
            sqlQuery.AddColumn<CaregiverMap>(nameof(CaregiverMap.Id), nameof(Caregiver.MapId));
            sqlQuery.AddColumn<CaregiverMap>(nameof(CaregiverMap.Description), nameof(Caregiver.Description));
            sqlQuery.AddJoin<CaregiverMap, CaregiverData>(nameof(CaregiverMap.CaregiverId), nameof(CaregiverData.Id));
            return sqlQuery;
        }

        private async Task<CaregiverMap> GetCaregiverMap(Caregiver caregiver, bool getNewId)
        {
            if (_pupilId is int pupilId)
            {

                int id = getNewId ? await _database.GetNextIdAsync<CaregiverMap>() : caregiver.MapId;
                CaregiverMap caregiverMap = new()
                {
                    Id = id,
                    CaregiverId = caregiver.Id,
                    PupilId = pupilId
                };
                return caregiverMap;
            }
            else
            {
                throw new Exception($"{nameof(PupilId)} is null");
            }
        }
    }
}