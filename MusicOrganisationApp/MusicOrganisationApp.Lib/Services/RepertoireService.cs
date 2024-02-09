﻿using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class RepertoireService : ISearchService<Repertoire>
    {
        private readonly DatabaseConnection _database;
        private int? _pupilId;

        public RepertoireService(DatabaseConnection database)
        {
            _database = database;
        }

        public int? PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public async Task DeleteAsync(Repertoire value)
        {
            DeleteStatement<RepertoireData> deleteStatement = new();
            deleteStatement.AddWhereEqual<RepertoireData>(nameof(RepertoireData.Id), value.Id);
            await _database.ExecuteAsync(deleteStatement);
        }

        public async Task<IEnumerable<Repertoire>> GetAllAsync()
        {
            SqlQuery<RepertoireData> sqlQuery = GetSqlQuery();
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            return repertoires;
        }

        public async Task InsertAsync(Repertoire value, bool getNewId)
        {
            RepertoireData repertoireData = await GetRepertoireData(value, getNewId);
            await _database.InsertAsync(repertoireData);
        }

        public async Task<IEnumerable<Repertoire>> SearchAsync(string search, string ordering)
        {
            SqlQuery<RepertoireData> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereLike<WorkData>(nameof(WorkData.Title), search);
            sqlQuery.AddAndEqual<RepertoireData>(nameof(Repertoire.PupilId), _pupilId);
            sqlQuery.AddOrderByAscending(ordering);
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            return repertoires;
        }

        public async Task<(bool, Repertoire)> TryGetAsync(int id)
        {
            SqlQuery<RepertoireData> sqlQuery = GetSqlQuery();
            sqlQuery.AddAndEqual<RepertoireData>(nameof(RepertoireData.Id), id);
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            return IService<Repertoire>.TryReturnValue(repertoires);
        }

        public async Task UpdateAsync(Repertoire value)
        {
            RepertoireData repertoireData = await GetRepertoireData(value, false);
            await _database.UpdateAsync(repertoireData);
        }

        private static SqlQuery<RepertoireData> GetSqlQuery()
        {
            SqlQuery<RepertoireData> sqlQuery = new(IService.DEFAULT_LIMIT);
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.Id), nameof(Repertoire.Id));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.DateStarted), nameof(Repertoire.DateStarted));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.Syllabus), nameof(Repertoire.Syllabus));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.IsFinishedLearning), nameof(Repertoire.IsFinishedLearning));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.Notes), nameof(Repertoire.Notes));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.PupilId), nameof(Repertoire.PupilId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Id), nameof(Repertoire.WorkId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Repertoire.Title));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Repertoire.Subtitle));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Repertoire.Genre));
            sqlQuery.AddColumn<Composer>(nameof(Composer.Id), nameof(Repertoire.ComposerId));
            sqlQuery.AddColumn<Composer>(nameof(Composer.Name), nameof(Repertoire.ComposerName));
            sqlQuery.AddInnerJoin<WorkData, RepertoireData>(nameof(WorkData.Id), nameof(RepertoireData.WorkId));
            sqlQuery.AddInnerJoin<Composer, WorkData>(nameof(Composer.Id), nameof(WorkData.ComposerId));

            return sqlQuery;
        }

        private async Task<RepertoireData> GetRepertoireData(Repertoire repertoire, bool getNewId)
        {
            int id = getNewId ? await _database.GetNextIdAsync<RepertoireData>() : repertoire.Id;
            RepertoireData repertoireData = new()
            {
                Id = id,
                PupilId = repertoire.PupilId,
                WorkId = repertoire.WorkId,
                DateStarted = repertoire.DateStarted,
                Syllabus = repertoire.Syllabus,
                IsFinishedLearning = repertoire.IsFinishedLearning,
                Notes = repertoire.Notes
            };
            return repertoireData;
        }
    }
}