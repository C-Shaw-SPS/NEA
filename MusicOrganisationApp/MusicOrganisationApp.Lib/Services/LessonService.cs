using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonService : LessonServiceBase<Lesson, LessonData>
    {
        private DateTime _date = DateTime.Today;

        public LessonService(IDatabaseConnection database) : base(database) { }

        public DateTime Date
        {
            get => _date;
            set => _date = value.Date;
        }

        protected override SqlQuery<LessonData> GetSqlQueryWithNoConditions()
        {
            SqlQuery<LessonData> sqlQuery = new();
            sqlQuery.AddField<LessonData>(nameof(LessonData.Id), nameof(Lesson.Id));
            sqlQuery.AddField<LessonData>(nameof(LessonData.PupilId), nameof(Lesson.PupilId));
            sqlQuery.AddField<LessonData>(nameof(LessonData.Date), nameof(Lesson.Date));
            sqlQuery.AddField<LessonData>(nameof(LessonData.StartTime), nameof(Lesson.StartTime));
            sqlQuery.AddField<LessonData>(nameof(LessonData.EndTime), nameof(Lesson.EndTime));
            sqlQuery.AddField<LessonData>(nameof(LessonData.Notes), nameof(Lesson.Notes));
            sqlQuery.AddField<Pupil>(nameof(Pupil.Name), nameof(Lesson.PupilName));
            sqlQuery.AddInnerJoin<Pupil, LessonData>(nameof(Pupil.Id), nameof(LessonData.PupilId));
            sqlQuery.AddOrderByAscending(nameof(Lesson.StartTime));
            return sqlQuery;
        }

        protected override SqlQuery<LessonData> GetAllSqlQuery()
        {
            SqlQuery<LessonData> sqlQuery = GetSqlQueryWithNoConditions();
            sqlQuery.AddWhereEqual<LessonData>(nameof(LessonData.Date), _date);
            return sqlQuery;
        }

        public override Task<IEnumerable<Lesson>> GetClashingLessonsAsync(object date, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            return GetClashingLessonsAsync(nameof(LessonData.Date), date, startTime, endTime, id);
        }

        protected override LessonData GetTableValue(Lesson value)
        {
            LessonData lessonData = new()
            {
                Id = value.Id,
                PupilId = value.PupilId,
                Date = value.Date,
                StartTime = value.StartTime,
                EndTime = value.EndTime,
                Notes = value.Notes
            };
            return lessonData;
        }

        public override async Task<(bool, Lesson)> TryGetAsync(int id)
        {
            SqlQuery<LessonData> sqlQuery = GetSqlQueryWithNoConditions();
            sqlQuery.AddWhereEqual<LessonData>(nameof(LessonData.Id), id);
            IEnumerable<Lesson> resuult = await _database.QueryAsync<Lesson>(sqlQuery);
            return IService<Lesson>.TryReturnValue(resuult);
        }
    }
}