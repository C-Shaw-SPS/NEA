using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class PupilAvailabilityService
    {
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

        public async Task<IEnumerable<LessonSlotData>> GetPupilAvailabilityAsync()
        {
            SqlQuery<LessonSlotData> sqlQuery = GetSqlQueryWithNoConditions();
            IEnumerable<LessonSlotData> pupilAvailability = await _database.QueryAsync<LessonSlotData>(sqlQuery);
            return pupilAvailability;
        }

        public async Task<IEnumerable<LessonSlotData>> GetUnusedLessonSlotsAsync()
        {
            IEnumerable<LessonSlotData> currentLessonSlots = await GetPupilAvailabilityAsync();
            IEnumerable<LessonSlotData> allLessonSlots = await _database.GetAllAsync<LessonSlotData>();
            IEnumerable<LessonSlotData> unusedLessonSlots = GetDifference(allLessonSlots, currentLessonSlots).Order();
            return unusedLessonSlots;
        }

        private static List<T> GetDifference<T>(IEnumerable<T> allItems, IEnumerable<T> itemsToRemove)
        {
            List<T> difference = [];
            foreach (T item in allItems)
            {
                if (!itemsToRemove.Contains(item))
                {
                    difference.Add(item);
                }
            }
            return difference;
        }

        public async Task AddAvailabilityAsync(LessonSlotData lessonSlotData)
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

        public async Task RemoveAvailabilityAsync(LessonSlotData lessonSlotData)
        {
            DeleteStatement<PupilAvailability> deleteStatement = new();
            deleteStatement.AddWhereEqual<PupilAvailability>(nameof(PupilAvailability.LessonSlotId), lessonSlotData.Id);
            deleteStatement.AddAndEqual<PupilAvailability>(nameof(PupilAvailability.PupilId), _pupilId);
            await _database.ExecuteAsync(deleteStatement);
        }

        private SqlQuery<LessonSlotData> GetSqlQueryWithNoConditions()
        {
            SqlQuery<LessonSlotData> sqlQuery = new();
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.Id));
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.DayOfWeek));
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.StartTime));
            sqlQuery.AddColumn<LessonSlotData>(nameof(LessonSlotData.EndTime));
            sqlQuery.AddInnerJoin<PupilAvailability, LessonSlotData>(nameof(PupilAvailability.LessonSlotId), nameof(LessonSlotData.Id));
            sqlQuery.AddWhereEqual<PupilAvailability>(nameof(PupilAvailability.PupilId), _pupilId);
            sqlQuery.AddOrderByAscending(nameof(LessonSlotData.DayOfWeek));
            sqlQuery.AddOrderByAscending(nameof(LessonSlotData.StartTime));
            return sqlQuery;
        }
    }
}