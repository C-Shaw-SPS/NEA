﻿using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilCaregiverService : IService<PupilCaregiver>
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
            CaregiverMap caregiverMap = await GetCaregiverMap(value, getNewId);
            await _database.InsertAsync(caregiverMap);
        }

        public async Task<IEnumerable<PupilCaregiver>> SearchAsync(string search, string ordering)
        {
            SqlQuery<CaregiverMap> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereEquals<CaregiverMap>(nameof(CaregiverMap.PupilId), _pupilId);
            sqlQuery.AddOrderBy(ordering);
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
            CaregiverMap caregiverMap = await GetCaregiverMap(value, false);
            await _database.UpdateAsync(caregiverMap);
        }

        private static SqlQuery<CaregiverMap> GetSqlQuery()
        {
            SqlQuery<CaregiverMap> sqlQuery = new();
            sqlQuery.AddColumn<CaregiverMap>(nameof(CaregiverMap.Id), nameof(PupilCaregiver.Id));
            sqlQuery.AddColumn<CaregiverMap>(nameof(CaregiverMap.Description), nameof(PupilCaregiver.Description));
            sqlQuery.AddColumn<CaregiverMap>(nameof(CaregiverMap.PupilId), nameof(PupilCaregiver.PupilId));
            sqlQuery.AddColumn<CaregiverMap>(nameof(CaregiverMap.CaregiverId), nameof(PupilCaregiver.CaregiverId));
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.Name), nameof(PupilCaregiver.Name));
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.Email), nameof(PupilCaregiver.Email));
            sqlQuery.AddColumn<CaregiverData>(nameof(CaregiverData.PhoneNumber), nameof(PupilCaregiver.PhoneNumber));
            sqlQuery.AddJoin<CaregiverData, CaregiverMap>(nameof(CaregiverData.Id), nameof(CaregiverMap.CaregiverId));
            return sqlQuery;
        }

        private async Task<CaregiverMap> GetCaregiverMap(PupilCaregiver caregiver, bool getNewId)
        {
            int id = getNewId ? await _database.GetNextIdAsync<CaregiverMap>() : caregiver.CaregiverId;
            CaregiverMap caregiverMap = new()
            {
                Id = id,
                CaregiverId = caregiver.Id,
                PupilId = caregiver.PupilId
            };
            return caregiverMap;
        }
    }
}