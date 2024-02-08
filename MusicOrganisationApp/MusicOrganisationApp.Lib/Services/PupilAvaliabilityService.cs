using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilAvaliabilityService
    {
        private readonly DatabaseConnection _database;
        private int? _pupilId;

        public PupilAvaliabilityService(DatabaseConnection database)
        {
            _database = database;
        }

        public int? PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public async Task<IEnumerable<LessonSlotData>> GetPupilAvaliabilityAsync()
        {
            SqlQuery<LessonSlotData> sqlQuery = GetSqlQueryWithNoConditions();
            IEnumerable<LessonSlotData> pupilAvaliability = await _database.QueryAsync<LessonSlotData>(sqlQuery);
            return pupilAvaliability;
        }

        public async Task<IEnumerable<LessonSlotData>> GetUnusedLessonSlotsAsync()
        {
            IEnumerable<LessonSlotData> currentLessonSlots = await GetPupilAvaliabilityAsync();
            IEnumerable<LessonSlotData> allLessonSlots = await _database.GetAllAsync<LessonSlotData>();
            IEnumerable<LessonSlotData> unusedLessonSlots = Enumerable.Except(currentLessonSlots, allLessonSlots).Order();
            return unusedLessonSlots;
        }

        public async Task AddAvaliabilityAsync(LessonSlotData lessonSlotData)
        {
            if (_pupilId is int pupilId)
            {
                int id = await _database.GetNextIdAsync<PupilAvaliability>();
                PupilAvaliability pupilAvaliability = new()
                {
                    Id = id,
                    PupilId = pupilId,
                    LessonSlotId = lessonSlotData.Id
                };
                await _database.InsertAsync(pupilAvaliability);
            }
        }

        public async Task RemoveAvaliabilityAsync(LessonSlotData lessonSlotData)
        {
            DeleteStatement<PupilAvaliability> deleteStatement = new();
            deleteStatement.AddCondition(nameof(PupilAvaliability.LessonSlotId), lessonSlotData.Id);
            await _database.ExecuteAsync(deleteStatement);
        }

        private SqlQuery<LessonSlotData> GetSqlQueryWithNoConditions()
        {
            SqlQuery<LessonSlotData> sqlQuery = new();
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.Id));
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.DayOfWeek));
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.StartTime));
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.EndTime));
            sqlQuery.AddInnerJoin<PupilAvaliability, LessonSlotData>(nameof(PupilAvaliability.LessonSlotId), nameof(LessonSlotData.Id));
            sqlQuery.AddWhereEquals<PupilAvaliability>(nameof(PupilAvaliability.PupilId), _pupilId);
            sqlQuery.AddOrderByAscending(nameof(LessonSlotData.DayOfWeek));
            sqlQuery.AddOrderByAscending(nameof(LessonSlotData.StartTime));
            return sqlQuery;
        }
    }
}