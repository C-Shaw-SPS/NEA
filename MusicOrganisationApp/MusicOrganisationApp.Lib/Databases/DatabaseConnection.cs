using SQLite;
using SQLitePCL;

namespace MusicOrganisationApp.Lib.Databases
{
    public class DatabaseConnection
    {
        private readonly string _path;
        private readonly SQLiteAsyncConnection _connection;

        public DatabaseConnection(string path)
        {
            _path = path.FormatAsDatabasePath();
            _connection = new(_path, DatabaseProperties.FLAGS);
        }

        public async Task CerateTableAsync<T>() where T : class, ITable, new()
        {
            await _connection.CreateTableAsync<T>();
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(ISqlStatement sqlStatement) where T : class, new()
        {
            string sql = sqlStatement.GetSql();
            IEnumerable<T> result = await _connection.QueryAsync<T>(sql);
            return result;
        }

        public async Task ExecuteAsync(ISqlStatement sqlStatement)
        {
            string sql = sqlStatement.GetSql();
            await _connection.ExecuteAsync(sql);
        }
    }
}