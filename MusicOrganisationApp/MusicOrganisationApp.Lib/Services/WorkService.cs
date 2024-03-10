using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class WorkService : ISearchService<Work>
    {
        private readonly IDatabaseConnection _database;

        public WorkService(IDatabaseConnection database)
        {
            _database = database;
        }

        public async Task DeleteAsync(Work value)
        {
            DeleteStatement<RepertoireData> deleteStatement = GetDeleteRepertoireQuery(value.Id);

            await _database.DeleteAsync<WorkData>(value.Id);
            await _database.ExecuteAsync(deleteStatement);
        }

        private static DeleteStatement<RepertoireData> GetDeleteRepertoireQuery(int workId)
        {
            DeleteStatement<RepertoireData> deleteStatement = new();
            deleteStatement.AddWhereEqual<RepertoireData>(nameof(RepertoireData.WorkId), workId);
            return deleteStatement;
        }

        public async Task<IEnumerable<Work>> GetAllAsync()
        {
            SqlQuery<WorkData> sqlQuery = GetSqlQuery();
            IEnumerable<Work> works = await _database.QueryAsync<Work>(sqlQuery);
            return works;
        }

        public async Task<(bool, Work)> TryGetAsync(int id)
        {
            SqlQuery<WorkData> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereEqual<WorkData>(nameof(WorkData.Id), id);
            IEnumerable<Work> result = await _database.QueryAsync<Work>(sqlQuery);
            if (result.Any())
            {
                return (true, result.First());
            }
            else
            {
                return (false, new());
            }
        }

        public async Task InsertAsync(Work value, bool getNewId)
        {
            WorkData workData = GetWorkData(value);
            await _database.InsertAsync(workData, getNewId);
        }

        public async Task<IEnumerable<Work>> SearchAsync(string search, string ordering)
        {
            SqlQuery<WorkData> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereLike<WorkData>(nameof(WorkData.Title), search);
            sqlQuery.AddOrderByAscending(ordering);

            IEnumerable<Work> works = await _database.QueryAsync<Work>(sqlQuery);
            return works;
        }

        public async Task UpdateAsync(Work value)
        {
            WorkData workData = GetWorkData(value);
            await _database.UpdateAsync(workData);
        }

        private static SqlQuery<WorkData> GetSqlQuery()
        {
            SqlQuery<WorkData> sqlQuery = new(IService.DEFAULT_LIMIT);
            sqlQuery.AddField<WorkData>(nameof(WorkData.Id), nameof(Work.Id));
            sqlQuery.AddField<WorkData>(nameof(WorkData.ComposerId), nameof(Work.ComposerId));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Title), nameof(Work.Title));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Subtitle), nameof(Work.Subtitle));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Genre), nameof(Work.Genre));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Notes), nameof(Work.Notes));
            sqlQuery.AddInnerJoin<Composer, WorkData>(nameof(Composer.Id), nameof(WorkData.ComposerId));
            sqlQuery.AddField<Composer>(nameof(Composer.Name), nameof(Work.ComposerName));

            return sqlQuery;
        }

        private WorkData GetWorkData(Work work)
        {
            WorkData workData = new()
            {
                Id = work.Id,
                ComposerId = work.ComposerId,
                Title = work.Title,
                Subtitle = work.Subtitle,
                Genre = work.Genre,
                Notes = work.Notes
            };
            return workData;
        }
    }
}
