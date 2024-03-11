using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class RepertoireService : ISearchService<Repertoire>
    {
        private readonly IDatabaseConnection _database;
        private int? _pupilId;

        public RepertoireService(IDatabaseConnection database)
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
            await _database.ExecuteAsync(deleteStatement, true);
        }

        public async Task<IEnumerable<Repertoire>> GetAllAsync()
        {
            SqlQuery<RepertoireData> sqlQuery = GetSqlQuery();
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            return repertoires;
        }

        public async Task InsertAsync(Repertoire value, bool getNewId)
        {
            RepertoireData repertoireData = GetRepertoireData(value);
            await _database.InsertAsync(repertoireData, getNewId);
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
            RepertoireData repertoireData = GetRepertoireData(value);
            await _database.UpdateAsync(repertoireData);
        }

        private static SqlQuery<RepertoireData> GetSqlQuery()
        {
            SqlQuery<RepertoireData> sqlQuery = new(IService.DEFAULT_LIMIT);
            sqlQuery.AddField<RepertoireData>(nameof(RepertoireData.Id), nameof(Repertoire.Id));
            sqlQuery.AddField<RepertoireData>(nameof(RepertoireData.DateStarted), nameof(Repertoire.DateStarted));
            sqlQuery.AddField<RepertoireData>(nameof(RepertoireData.Syllabus), nameof(Repertoire.Syllabus));
            sqlQuery.AddField<RepertoireData>(nameof(RepertoireData.IsFinishedLearning), nameof(Repertoire.IsFinishedLearning));
            sqlQuery.AddField<RepertoireData>(nameof(RepertoireData.Notes), nameof(Repertoire.Notes));
            sqlQuery.AddField<RepertoireData>(nameof(RepertoireData.PupilId), nameof(Repertoire.PupilId));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Id), nameof(Repertoire.WorkId));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Title), nameof(Repertoire.Title));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Subtitle), nameof(Repertoire.Subtitle));
            sqlQuery.AddField<WorkData>(nameof(WorkData.Genre), nameof(Repertoire.Genre));
            sqlQuery.AddField<Composer>(nameof(Composer.Id), nameof(Repertoire.ComposerId));
            sqlQuery.AddField<Composer>(nameof(Composer.Name), nameof(Repertoire.ComposerName));
            sqlQuery.AddInnerJoin<WorkData, RepertoireData>(nameof(WorkData.Id), nameof(RepertoireData.WorkId));
            sqlQuery.AddInnerJoin<Composer, WorkData>(nameof(Composer.Id), nameof(WorkData.ComposerId));

            return sqlQuery;
        }

        private RepertoireData GetRepertoireData(Repertoire repertoire)
        {
            RepertoireData repertoireData = new()
            {
                Id = repertoire.Id,
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