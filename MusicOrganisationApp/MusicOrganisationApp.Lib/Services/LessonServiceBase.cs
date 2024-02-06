using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Lib.Services
{
    public abstract class LessonServiceBase<T> : IService<T> where T : class, ILesson, ITable, new()
    {
        protected readonly DatabaseConnection _database;

        public LessonServiceBase(DatabaseConnection database)
        {
            _database = database;
        }

        protected async Task<IEnumerable<T>> GetClashingLessonsAsync(string dateColumn, object date, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            SqlQuery<T> sqlQuery = GetClashingLessonsSqlQuery(dateColumn, date, startTime, endTime, id);
            IEnumerable<T> clashingLessons = await _database.QueryAsync<T>(sqlQuery);
            return clashingLessons;
        }

        private static SqlQuery<T> GetClashingLessonsSqlQuery(string dateColumn, object date, TimeSpan startTime, TimeSpan endTime, int? id)
        {
            SqlQuery<T> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<T>(dateColumn, date);
            sqlQuery.AddAndNotEqual<T>(nameof(ITable.Id), id);
            sqlQuery.AddAndLessOrEqual<T>(nameof(ILesson.StartTime), startTime);
            sqlQuery.AddAndGreaterThan<T>(nameof(ILesson.EndTime), startTime);
            sqlQuery.AddOrEqual<T>(dateColumn, date);
            sqlQuery.AddAndNotEqual<T>(nameof(ITable.Id), id);
            sqlQuery.AddAndGreaterOrEqual<T>(nameof(ILesson.StartTime), startTime);
            sqlQuery.AddAndLessThan<T>(nameof(ILesson.StartTime), endTime);
            return sqlQuery;
        }

        public abstract Task<IEnumerable<T>> GetClashingLessonsAsync(object date, TimeSpan startTime, TimeSpan endTime, int? id);

        public async Task DeleteAsync(T value)
        {
            await _database.DeleteAsync(value);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            SqlQuery<T> sqlQuery = GetAllSqlQuery();
            IEnumerable<T> lessons = await _database.QueryAsync<T>(sqlQuery);
            return lessons;
        }

        protected abstract SqlQuery<T> GetAllSqlQuery();

        public async Task InsertAsync(T value, bool getNewId)
        {
            if (getNewId)
            {
                int id = await _database.GetNextIdAsync<T>();
                value.Id = id;
            }
            await _database.InsertAsync(value);
        }

        public async Task<(bool, T)> TryGetAsync(int id)
        {
            (bool suceeded, T value) result = await _database.TryGetAsync<T>(id);
            return result;
        }

        public async Task UpdateAsync(T value)
        {
            await _database.UpdateAsync(value);
        }
    }
}
