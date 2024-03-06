using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilAvailabilityService
    {
        private const string _ALL_LESSON_SLOTS = nameof(_ALL_LESSON_SLOTS);
        private const string _USED_LESSON_SLOTS = nameof(_USED_LESSON_SLOTS);

        private readonly DatabaseConnection _database;
        private int? _pupilId;

        public PupilAvailabilityService(DatabaseConnection database)
        {
            _database = database;
        }

        public int? PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public async Task<IEnumerable<LessonSlot>> GetPupilAvailabilityAsync()
        {
            SqlQuery<LessonSlot> sqlQuery = GetSqlQueryWithNoConditions();
            IEnumerable<LessonSlot> pupilAvailability = await _database.QueryAsync<LessonSlot>(sqlQuery);
            return pupilAvailability;
        }

        public async Task<IEnumerable<LessonSlot>> GetUnusedLessonSlotsAsync()
        {
            string sqlQuery = GetUnusedLessonSlotSqlQuery();
            await _database.CreateTableAsync<LessonSlot>();
            await _database.CreateTableAsync<PupilAvailability>();
            IEnumerable<LessonSlot> unusedLessonSlots = await _database.QueryAsync<LessonSlot>(sqlQuery);
            return unusedLessonSlots;
        }

        private string GetUnusedLessonSlotSqlQuery()
        {
            string sqlQuery = $"""
                SELECT * FROM {LessonSlot.TableName} {_ALL_LESSON_SLOTS}
                WHERE {_ALL_LESSON_SLOTS}.{nameof(LessonSlot.Id)} NOT IN
                (
                    SELECT {_USED_LESSON_SLOTS}.{nameof(LessonSlot.Id)} FROM {LessonSlot.TableName} {_USED_LESSON_SLOTS}
                    JOIN {PupilAvailability.TableName} ON {PupilAvailability.TableName}.{nameof(PupilAvailability.LessonSlotId)} = {_USED_LESSON_SLOTS}.{nameof(LessonSlot.Id)}
                    WHERE {PupilAvailability.TableName}.{nameof(PupilAvailability.PupilId)} = {_pupilId}
                )
                ORDER BY {_ALL_LESSON_SLOTS}.{nameof(LessonSlot.DayOfWeek)} ASC, {_ALL_LESSON_SLOTS}.{nameof(LessonSlot.StartTime)} ASC
                """;
            return sqlQuery;
        }

        public async Task AddAvailabilityAsync(LessonSlot lessonSlotData)
        {
            if (_pupilId is int pupilId)
            {
                int id = await _database.GetNextIdAsync<PupilAvailability>();
                PupilAvailability pupilAvailability = new()
                {
                    Id = id,
                    PupilId = pupilId,
                    LessonSlotId = lessonSlotData.Id
                };
                await _database.InsertAsync(pupilAvailability);
            }
        }

        public async Task RemoveAvailabilityAsync(LessonSlot lessonSlotData)
        {
            DeleteStatement<PupilAvailability> deleteStatement = new();
            deleteStatement.AddWhereEqual<PupilAvailability>(nameof(PupilAvailability.LessonSlotId), lessonSlotData.Id);
            deleteStatement.AddAndEqual<PupilAvailability>(nameof(PupilAvailability.PupilId), _pupilId);
            await _database.ExecuteAsync(deleteStatement);
        }

        private SqlQuery<LessonSlot> GetSqlQueryWithNoConditions()
        {
            SqlQuery<LessonSlot> sqlQuery = new();
            sqlQuery.AddColumn<LessonSlot>(nameof(LessonSlot.Id));
            sqlQuery.AddColumn<LessonSlot>(nameof(LessonSlot.DayOfWeek));
            sqlQuery.AddColumn<LessonSlot>(nameof(LessonSlot.StartTime));
            sqlQuery.AddColumn<LessonSlot>(nameof(LessonSlot.EndTime));
            sqlQuery.AddInnerJoin<PupilAvailability, LessonSlot>(nameof(PupilAvailability.LessonSlotId), nameof(LessonSlot.Id));
            sqlQuery.AddWhereEqual<PupilAvailability>(nameof(PupilAvailability.PupilId), _pupilId);
            sqlQuery.AddOrderByAscending(nameof(LessonSlot.DayOfWeek));
            sqlQuery.AddOrderByAscending(nameof(LessonSlot.StartTime));
            return sqlQuery;
        }
    }
}