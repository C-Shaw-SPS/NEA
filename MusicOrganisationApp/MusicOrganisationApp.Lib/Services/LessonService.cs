using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class LessonService : LessonServiceBase<LessonData>
    {
        private DateTime _date = DateTime.Today;

        public LessonService(DatabaseConnection database) : base(database) { }

        public DateTime Date
        {
            get => _date;
            set => _date = value.Date;
        }

        protected override SqlQuery<LessonData> GetAllSqlQuery()
        {
            SqlQuery<LessonData> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<LessonData>(nameof(LessonData.Date), _date);
            return sqlQuery;
        }

        public override Task<IEnumerable<LessonData>> GetClashingLessonsAsync(object date, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            return GetClashingLessonsAsync(nameof(LessonData.Date), date, startTime, endTime, id);
        }
    }
}