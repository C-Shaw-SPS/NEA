using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class RepertoireService : IService<Repertoire>
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
            deleteStatement.AddCondition(nameof(RepertoireData.Id), value.Id);
            await _database.ExecuteAsync(deleteStatement);
        }

        public async Task<IEnumerable<Repertoire>> GetAllAsync()
        {
            SqlQuery sqlQuery = GetSqlQuery();
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            return repertoires;
        }

        public async Task InsertAsync(Repertoire value, bool getNewId)
        {
            RepertoireData repertoireData = await GetRepertoireData(value, true);
            await _database.InsertAsync(repertoireData);
        }

        public async Task<IEnumerable<Repertoire>> SearchAsync(string search, string ordering)
        {
            SqlQuery sqlQuery = GetSqlQuery();
            sqlQuery.AddWhereLike<WorkData>(nameof(WorkData.Title), search);
            sqlQuery.AddOrderBy(ordering);
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            return repertoires;
        }

        public async Task<(bool, Repertoire)> TryGetAsync(int id)
        {
            SqlQuery sqlQuery = GetSqlQuery();
            sqlQuery.AddAndEqual<RepertoireData>(nameof(RepertoireData.Id), id);
            IEnumerable<Repertoire> repertoires = await _database.QueryAsync<Repertoire>(sqlQuery);
            if (repertoires.Any())
            {
                return (true, repertoires.First());
            }
            else
            {
                return (false, new());
            }
        }

        public async Task UpdateAsync(Repertoire value)
        {
            RepertoireData repertoireData = await GetRepertoireData(value, false);
            await _database.UpdateAsync(repertoireData);
        }

        private SqlQuery<RepertoireData> GetSqlQuery()
        {
            SqlQuery<RepertoireData> sqlQuery = new(SqlQuery.DEFAULT_LIMIT);
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.Id), nameof(Repertoire.Id));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.DateStarted), nameof(Repertoire.DateStarted));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.Syllabus), nameof(Repertoire.Syllabus));
            sqlQuery.AddColumn<RepertoireData>(nameof(RepertoireData.IsFinishedLearning), nameof(Repertoire.IsFinishedLearning));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Id), nameof(Repertoire.WorkId));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Title), nameof(Repertoire.Title));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Subtitle), nameof(Repertoire.Subtitle));
            sqlQuery.AddColumn<WorkData>(nameof(WorkData.Genre), nameof(Repertoire.Genre));
            sqlQuery.AddColumn<ComposerData>(nameof(ComposerData.Id), nameof(Repertoire.ComposerId));
            sqlQuery.AddColumn<ComposerData>(nameof(ComposerData.Name), nameof(Repertoire.ComposerName));
            sqlQuery.AddJoin<WorkData, RepertoireData>(nameof(WorkData.Id), nameof(RepertoireData.WorkId));
            sqlQuery.AddJoin<ComposerData, WorkData>(nameof(ComposerData.Id), nameof(WorkData.ComposerId));
            sqlQuery.AddWhereEquals<RepertoireData>(nameof(RepertoireData.PupilId), _pupilId);

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