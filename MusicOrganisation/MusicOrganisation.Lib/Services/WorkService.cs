using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Models;

namespace MusicOrganisation.Lib.Services
{
    public class WorkService : IService<Work>
    {
        private readonly Database _database;

        public WorkService(Database database)
        {
            _database = database;
        }

        public Task DeleteAsync(Work value)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Work>> GetAllAsync()
        {
            SqlQuery sqlQuery = GetSqlQuery();
            IEnumerable<Work> works = await _database.QueryAsync<Work>(sqlQuery);
            return works;
        }

        private SqlQuery GetSqlQuery()
        {
            SqlQuery<WorkData> query = new(SqlQuery.DEFAULT_LIMIT);
            query.AddColumn<WorkData>(nameof(WorkData.Id), nameof(Work.WorkId));
            query.AddColumn<WorkData>(nameof(WorkData.ComposerId), nameof(Work.ComposerId));
            query.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Work.Title));
            query.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Work.Subtitle));
            query.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Work.Genre));
            query.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(Work.ComposerName));
            query.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            query.AddWhereEquals<WorkData>(nameof(WorkData.IsDeleted), false);
            return query;
        }

        public async Task InitialiseData()
        {
            IEnumerable<WorkData> works = await WorkGetter.GetFromOpenOpus();
            await _database.InsertAllAsync(works);
        }

        public Task UpdateAsync(Work value)
        {
            throw new NotImplementedException();
        }
    }
}