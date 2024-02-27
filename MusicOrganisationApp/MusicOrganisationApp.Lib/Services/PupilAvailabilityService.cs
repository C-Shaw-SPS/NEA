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

        public async Task<IEnumerable<LessonSlot>> GetPupilAvailabilityAsync()
        {
            SqlQuery<LessonSlot> sqlQuery = GetSqlQueryWithNoConditions();
            IEnumerable<LessonSlot> pupilAvailability = await _database.QueryAsync<LessonSlot>(sqlQuery);
            return pupilAvailability;
        }

        public async Task<IEnumerable<LessonSlot>> GetUnusedLessonSlotsAsync()
        {
            IEnumerable<LessonSlot> currentLessonSlots = await GetPupilAvailabilityAsync();
            IEnumerable<LessonSlot> allLessonSlots = await _database.GetAllAsync<LessonSlot>();
            IEnumerable<LessonSlot> unusedLessonSlots = GetDifference(allLessonSlots, currentLessonSlots).Order();
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