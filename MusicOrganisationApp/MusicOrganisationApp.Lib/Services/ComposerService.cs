﻿using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class ComposerService : ISearchService<ComposerData>
    {
        private readonly DatabaseConnection _database;

        public ComposerService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task<IEnumerable<ComposerData>> GetAllAsync()
        {
            IEnumerable<ComposerData> composers = await _database.GetAllAsync<ComposerData>();
            return composers;
        }

        public async Task DeleteAsync(ComposerData composer)
        {
            DeleteStatement<WorkData> deleteWorksStatement = GetDeleteWorksStatement(composer);
            DeleteStatement<RepertoireData> deleteRepertoireStatement = await GetDeleteRepertoireStatement(composer);

            await _database.DeleteAsync(composer);
            await _database.ExecuteAsync(deleteWorksStatement);
            await _database.ExecuteAsync(deleteRepertoireStatement);

        }

        private static DeleteStatement<WorkData> GetDeleteWorksStatement(ComposerData composer)
        {
            DeleteStatement<WorkData> deleteStatement = new();
            deleteStatement.AddWhereEqual(nameof(WorkData.ComposerId), composer.Id);
            return deleteStatement;
        }

        private async Task<DeleteStatement<RepertoireData>> GetDeleteRepertoireStatement(ComposerData composer)
        {
            IEnumerable<int> workIds = await GetWorkIdsAsync(composer);
            DeleteStatement<RepertoireData> deleteStatement = new();
            if (workIds.Any())
            {
                deleteStatement.AddWhereEqual(nameof(RepertoireData.WorkId), workIds.First());
                foreach (int workId in workIds.Skip(1))
                {
                    deleteStatement.AddOrEqual(nameof(RepertoireData.WorkId), workId);
                }
            }

            return deleteStatement;
        }

        private async Task<IEnumerable<int>> GetWorkIdsAsync(ComposerData composer)
        {
            SqlQuery<WorkData> sqlQuery = new();
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Id));
            sqlQuery.AddWhereEquals<WorkData>(nameof(WorkData.ComposerId), composer.Id);
            IEnumerable<WorkData> works = await _database.QueryAsync<WorkData>(sqlQuery);
            IEnumerable<int> workIds = works.Select(work => work.Id);
            return workIds;
        }

        public async Task UpdateAsync(ComposerData value)
        {
            await _database.UpdateAsync(value);
        }

        public async Task InsertAsync(ComposerData value, bool getNewId)
        {
            if (getNewId)
            {
                int id = await _database.GetNextIdAsync<ComposerData>();
                value.Id = id;
            }
            await _database.InsertAsync(value);
        }

        public async Task<IEnumerable<ComposerData>> SearchAsync(string search, string ordering)
        {
            SqlQuery<ComposerData> query = new(IService.DEFAULT_LIMIT) { SelectAll = true };
            query.AddWhereLike<ComposerData>(nameof(ComposerData.Name), search);
            query.AddOrderByAscending(ordering);

            IEnumerable<ComposerData> composers = await _database.QueryAsync<ComposerData>(query);
            return composers;
        }

        public async Task<(bool, ComposerData)> TryGetAsync(int id)
        {
            (bool suceeded, ComposerData compser) value = await _database.TryGetAsync<ComposerData>(id);
            return value;
        }
    }
}