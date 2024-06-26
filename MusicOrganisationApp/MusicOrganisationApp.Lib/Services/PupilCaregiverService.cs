﻿using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilCaregiverService : ISearchService<PupilCaregiver>
    {
        private readonly IDatabaseConnection _database;
        private int? _pupilId;

        public PupilCaregiverService(IDatabaseConnection database)
        {
            _database = database;
        }

        public int? PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public async Task DeleteAsync(PupilCaregiver value)
        {
            await _database.DeleteAsync<CaregiverMap>(value.CaregiverId);
        }

        public async Task<IEnumerable<PupilCaregiver>> GetAllAsync()
        {
            SqlQuery<CaregiverMap> sqlQuery = GetSqlQuery();
            IEnumerable<PupilCaregiver> caregivers = await _database.QueryAsync<PupilCaregiver>(sqlQuery);
            return caregivers;
        }

        public async Task InsertAsync(PupilCaregiver value, bool getNewId)
        {
            CaregiverMap caregiverMap = GetCaregiverMap(value);
            await _database.InsertAsync(caregiverMap, getNewId);
        }

        public async Task<IEnumerable<PupilCaregiver>> SearchAsync(string search, string ordering)
        {
            SqlQuery<CaregiverMap> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereEqual<CaregiverMap>(nameof(CaregiverMap.PupilId), _pupilId);
            sqlQuery.AddOrderByAscending(ordering);
            IEnumerable<PupilCaregiver> caregivers = await _database.QueryAsync<PupilCaregiver>(sqlQuery);
            return caregivers;
        }

        public async Task<(bool, PupilCaregiver)> TryGetAsync(int id)
        {
            SqlQuery<CaregiverMap> sqlQuery = GetSqlQuery();
            IEnumerable<PupilCaregiver> caregivers = await _database.QueryAsync<PupilCaregiver>(sqlQuery);
            return IService<PupilCaregiver>.TryReturnValue(caregivers);
        }

        public async Task UpdateAsync(PupilCaregiver value)
        {
            CaregiverMap caregiverMap = GetCaregiverMap(value);
            await _database.UpdateAsync(caregiverMap);
        }

        private static SqlQuery<CaregiverMap> GetSqlQuery()
        {
            SqlQuery<CaregiverMap> sqlQuery = new();
            sqlQuery.AddField<CaregiverMap>(nameof(CaregiverMap.Id), nameof(PupilCaregiver.Id));
            sqlQuery.AddField<CaregiverMap>(nameof(CaregiverMap.Description), nameof(PupilCaregiver.Description));
            sqlQuery.AddField<CaregiverMap>(nameof(CaregiverMap.PupilId), nameof(PupilCaregiver.PupilId));
            sqlQuery.AddField<CaregiverMap>(nameof(CaregiverMap.CaregiverId), nameof(PupilCaregiver.CaregiverId));
            sqlQuery.AddField<CaregiverData>(nameof(CaregiverData.Name), nameof(PupilCaregiver.Name));
            sqlQuery.AddField<CaregiverData>(nameof(CaregiverData.EmailAddress), nameof(PupilCaregiver.EmailAddress));
            sqlQuery.AddField<CaregiverData>(nameof(CaregiverData.PhoneNumber), nameof(PupilCaregiver.PhoneNumber));
            sqlQuery.AddInnerJoin<CaregiverData, CaregiverMap>(nameof(CaregiverData.Id), nameof(CaregiverMap.CaregiverId));
            return sqlQuery;
        }

        private CaregiverMap GetCaregiverMap(PupilCaregiver pupilCaregiver)
        {
            CaregiverMap caregiverMap = new()
            {
                Id = pupilCaregiver.Id,
                CaregiverId = pupilCaregiver.CaregiverId,
                PupilId = pupilCaregiver.PupilId,
                Description = pupilCaregiver.Description
            };
            return caregiverMap;
        }
    }
}