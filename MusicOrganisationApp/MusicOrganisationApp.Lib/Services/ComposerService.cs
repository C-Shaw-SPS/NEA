using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class ComposerService : ISearchService<Composer>
    {
        private readonly IDatabaseConnection _database;

        public ComposerService(IDatabaseConnection database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Composer>> GetAllAsync()
        {
            IEnumerable<Composer> composers = await _database.GetAllAsync<Composer>();
            return composers;
        }

        public async Task DeleteAsync(Composer composer)
        {
            DeleteStatement<WorkData> deleteWorksStatement = GetDeleteWorksStatement(composer);
            DeleteStatement<RepertoireData> deleteRepertoireStatement = await GetDeleteRepertoireStatement(composer);

            await _database.DeleteAsync(composer);
            await _database.ExecuteAsync(deleteWorksStatement);
            await _database.ExecuteAsync(deleteRepertoireStatement);

        }

        private static DeleteStatement<WorkData> GetDeleteWorksStatement(Composer composer)
        {
            DeleteStatement<WorkData> deleteStatement = new();
            deleteStatement.AddWhereEqual<WorkData>(nameof(WorkData.ComposerId), composer.Id);
            return deleteStatement;
        }

        private async Task<DeleteStatement<RepertoireData>> GetDeleteRepertoireStatement(Composer composer)
        {
            IEnumerable<int> workIds = await GetWorkIdsAsync(composer);
            DeleteStatement<RepertoireData> deleteStatement = new();
            if (workIds.Any())
            {
                deleteStatement.AddWhereEqual<RepertoireData>(nameof(RepertoireData.WorkId), workIds.First());
                foreach (int workId in workIds.Skip(1))
                {
                    deleteStatement.AddOrEqual<RepertoireData>(nameof(RepertoireData.WorkId), workId);
                }
            }

            return deleteStatement;
        }

        private async Task<IEnumerable<int>> GetWorkIdsAsync(Composer composer)
        {
            SqlQuery<WorkData> sqlQuery = new();
            sqlQuery.AddField<WorkData>(nameof(WorkData.Id));
            sqlQuery.AddWhereEqual<WorkData>(nameof(WorkData.ComposerId), composer.Id);
            IEnumerable<WorkData> works = await _database.QueryAsync<WorkData>(sqlQuery);
            IEnumerable<int> workIds = works.Select(work => work.Id);
            return workIds;
        }

        public async Task UpdateAsync(Composer value)
        {
            await _database.UpdateAsync(value);
        }

        public async Task InsertAsync(Composer value, bool getNewId)
        {
            await _database.InsertAsync(value, getNewId);
        }

        public async Task<IEnumerable<Composer>> SearchAsync(string search, string ordering)
        {
            SqlQuery<Composer> query = new(IService.DEFAULT_LIMIT) { SelectAll = true };
            query.AddWhereLike<Composer>(nameof(Composer.Name), search);
            query.AddOrderByAscending(ordering);

            IEnumerable<Composer> composers = await _database.QueryAsync<Composer>(query);
            return composers;
        }

        public async Task<(bool, Composer)> TryGetAsync(int id)
        {
            (bool suceeded, Composer compser) value = await _database.TryGetAsync<Composer>(id);
            return value;
        }
    }
}