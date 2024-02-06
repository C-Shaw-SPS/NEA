using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Lib.Services
{
    public abstract class LessonServiceBase<TModel, TTable> : IService<TModel> where TModel : class, ILesson<TTable>, new() where TTable : class, ITable, new()
    {
        protected readonly DatabaseConnection _database;

        public LessonServiceBase(DatabaseConnection database)
        {
            _database = database;
        }

        protected async Task<IEnumerable<TModel>> GetClashingLessonsAsync(string dateColumn, object date, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            SqlQuery<TTable> sqlQuery = GetClashingLessonsSqlQuery(dateColumn, date, startTime, endTime, id);
            IEnumerable<TModel> clashingLessons = await _database.QueryAsync<TModel>(sqlQuery);
            return clashingLessons;
        }

        private static SqlQuery<TTable> GetClashingLessonsSqlQuery(string dateColumn, object date, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            SqlQuery<TTable> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<TTable>(dateColumn, date);
            sqlQuery.AddAndNotEqual<TTable>(nameof(ITable.Id), id);
            sqlQuery.AddAndLessOrEqual<TTable>(nameof(ILesson<TTable>.StartTime), startTime);
            sqlQuery.AddAndGreaterThan<TTable>(nameof(ILesson<TTable>.EndTime), startTime);
            sqlQuery.AddOrEqual<TTable>(dateColumn, date);
            sqlQuery.AddAndNotEqual<TTable>(nameof(ITable.Id), id);
            sqlQuery.AddAndGreaterOrEqual<TTable>(nameof(ILesson<TTable>.StartTime), startTime);
            sqlQuery.AddAndLessThan<TTable>(nameof(ILesson<TTable>.StartTime), endTime);
            return sqlQuery;
        }

        public abstract Task<IEnumerable<TModel>> GetClashingLessonsAsync(object date, TimeSpan startTime, TimeSpan endTime, int? id);

        public async Task DeleteAsync(TModel value)
        {
            await _database.DeleteAsync<TTable>(value.Id);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            SqlQuery<TTable> sqlQuery = GetAllSqlQuery();
            IEnumerable<TModel> lessons = await _database.QueryAsync<TModel>(sqlQuery);
            return lessons;
        }

        protected abstract SqlQuery<TTable> GetAllSqlQuery();

        public async Task InsertAsync(TModel value, bool getNewId)
        {
            TTable tableValue = GetTableValue(value);
            if (getNewId)
            {
                int id = await _database.GetNextIdAsync<TTable>();
                value.Id = id;
            }
            await _database.InsertAsync(tableValue);
        }

        protected abstract TTable GetTableValue(TModel value);

        public abstract Task<(bool, TModel)> TryGetAsync(int id);

        public async Task UpdateAsync(TModel value)
        {
            TTable tableValue = GetTableValue(value);
            await _database.UpdateAsync(tableValue);
        }
    }
}