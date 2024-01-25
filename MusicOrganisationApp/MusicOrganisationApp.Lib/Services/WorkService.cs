using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class WorkService : IService<Work>
    {
        private readonly DatabaseConnection _database;

        public WorkService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task DeleteAsync(Work value)
        {
            DeleteStatement<RepertoireData> deleteStatement = GetDeleteRepertoireQuery(value.WorkId);

            await _database.DeleteAsync<WorkData>(value.WorkId);
            await _database.ExecuteAsync(deleteStatement);
        }

        private static DeleteStatement<RepertoireData> GetDeleteRepertoireQuery(int workId)
        {
            DeleteStatement<RepertoireData> deleteStatement = new();
            deleteStatement.AddCondition(nameof(RepertoireData.WorkId), workId);
            return deleteStatement;
        }

        public async Task<IEnumerable<Work>> GetAllAsync()
        {
            SqlQuery<WorkData> sqlQuery = GetSqlQuery();
            IEnumerable<Work> works = await _database.QueryAsync<Work>(sqlQuery);
            return works;
        }

        public async Task<(bool, Work)> GetAsync(int id)
        {
            SqlQuery<WorkData> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereEquals<WorkData>(nameof(WorkData.Id), id);
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
            WorkData workData = await GetWorkData(value, getNewId);
            await _database.InsertAsync(workData);
        }

        public async Task<IEnumerable<Work>> SearchAsync(string search, string ordering)
        {
            SqlQuery<WorkData> sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereLike<WorkData>(nameof(WorkData.Title), search);

            if (WorkData.GetColumnNames().Contains(ordering))
            {
                sqlQuery.AddOrderBy<WorkData>(ordering);
            }
            else
            {
                sqlQuery.AddOrderBy<ComposerData>(ordering);
            }

            IEnumerable<Work> works = await _database.QueryAsync<Work>(sqlQuery);
            return works;
        }

        public async Task UpdateAsync(Work value)
        {
            WorkData workData = await GetWorkData(value, false);
            await _database.UpdateAsync(workData);
        }

        private static SqlQuery<WorkData> GetSqlQuery()
        {
            SqlQuery<WorkData> sqlQuery = new();
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Id), nameof(Work.WorkId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.ComposerId), nameof(Work.ComposerId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Work.Title));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Work.Subtitle));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Work.Genre));
            sqlQuery.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            sqlQuery.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(Work.ComposerName));

            return sqlQuery;
        }

        private async Task<WorkData> GetWorkData(Work work, bool getNewId)
        {
            int id = getNewId ? await _database.GetNextIdAsync<WorkData>() : work.WorkId;
            WorkData workData = new()
            {
                Id = id,
                ComposerId = work.ComposerId,
                Title = work.Title,
                Subtitle = work.Subtitle,
                Genre = work.Genre
            };
            return workData;
        }
    }
}
