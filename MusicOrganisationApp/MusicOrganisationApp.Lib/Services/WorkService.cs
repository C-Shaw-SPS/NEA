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
    public class WorkService : IService<Work>
    {
        private readonly DatabaseConnection _database;
        private int? _composerId;

        public WorkService(DatabaseConnection database)
        {
            _database = database;
        }

        public int? ComposerId
        {
            get => _composerId;
            set => _composerId = value;
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

        private SqlQuery<WorkData> GetSqlQuery()
        {
            SqlQuery<WorkData> sqlQuery = new();
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Id), nameof(Work.WorkId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.ComposerId), nameof(Work.ComposerId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Work.Title));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Work.Subtitle));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Work.Genre));
            sqlQuery.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            sqlQuery.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(Work.ComposerName));

            if (_composerId is not null)
            {
                sqlQuery.AddWhereEquals<WorkData>(nameof(WorkData.ComposerId), _composerId);
            }

            return sqlQuery;
        }

        public Task<(bool, Work)> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Work value, bool getNewId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Work>> SearchAsync(string search, string ordering)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Work value)
        {
            throw new NotImplementedException();
        }
    }
}
