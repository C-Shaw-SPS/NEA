using MusicOrganisationApp.Lib.Databases;
using System.Runtime.CompilerServices;

namespace MusicOrganisationApp.Lib.Models
{
    public interface ILesson
    {
        public TimeSpan StartTime { get; }

        public TimeSpan EndTime { get; }

        public static async Task<IEnumerable<T>> GetClashingLessons<T>(DatabaseConnection database, string dateColumn, object date, TimeSpan startTime, TimeSpan endTime, int? id) where T : class, ILesson, ITable, new()
        {
            SqlQuery<T> sqlQuery = GetClashingLessonsSqlQuery<T>(dateColumn, date, startTime, endTime, id);
            IEnumerable<T> clashingLessons = await database.QueryAsync<T>(sqlQuery);
            return clashingLessons;
        }

        private static SqlQuery<T> GetClashingLessonsSqlQuery<T>(string dateColumn, object date, TimeSpan startTime, TimeSpan endTime, int? id) where T : class, ILesson, ITable, new()
        {
            SqlQuery<T> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEquals<T>(dateColumn, date);
            sqlQuery.AddAndNotEqual<T>(nameof(ITable.Id), id);
            sqlQuery.AddAndLessOrEqual<T>(nameof(StartTime), startTime);
            sqlQuery.AddAndGreaterThan<T>(nameof(EndTime), startTime);
            sqlQuery.AddOrEqual<T>(dateColumn, date);
            sqlQuery.AddAndNotEqual<T>(nameof(ITable.Id), id);
            sqlQuery.AddAndGreaterOrEqual<T>(nameof(StartTime), startTime);
            sqlQuery.AddAndLessThan<T>(nameof(StartTime), endTime);
            return sqlQuery;
        }
    }
}