using OpenOpusDatabase.Lib.Models;
using SQLite;

namespace OpenOpusDatabase.Lib.Databases
{
    public class Database<T> where T : class, IIdentifiable, new()
    {
        private SQLiteAsyncConnection _connection;
        private string _path;

        public Database(string path)
        {
            _path = FormatDatabasePath(path);
        }

        protected SQLiteAsyncConnection Connection => _connection;

        private static string FormatDatabasePath(string path)
        {
            if (path.EndsWith(".db"))
            {
                return path;
            }
            else
            {
                return path + ".db";
            }
        }

        protected async Task InitAsync()
        {
            if (_connection is not null)
            {
                return;
            }

            _connection = new SQLiteAsyncConnection(_path, DatabaseProperties.FLAGS);
            await _connection.CreateTableAsync<T>();
        }

        public async Task InsertAsync(T value)
        {
            await InitAsync();
            await _connection.InsertAsync(value);
        }

        public async Task InsertAllAsync(IEnumerable<T> values)
        {
            await InitAsync();
            await _connection.InsertAllAsync(values);
        }

        public async Task DeleteAsync(T value)
        {
            await InitAsync();
            await _connection.DeleteAsync(value);
        }

        public async Task<List<T>> GetAllAsync()
        {
            await InitAsync();
            List<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {TableName.GetTableName<T>()}");
            return result;
        }

        public async Task<T> GetAsync(int id)
        {
            await InitAsync();
            T result = await _connection
                .Table<T>()
                .Where(value => value.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task ClearAsync()
        {
            await InitAsync();
            await _connection.DeleteAllAsync<T>();
        }

        public async Task UpdateAsync(T value)
        {
            await InitAsync();
            await _connection.UpdateAsync(value);
        }

        public async Task<List<int>> GetIdsAsync()
        {
            List<int> ids = (await GetAllAsync())
                .Select(property => property.Id)
                .ToList();
            return ids;
        }
    }
}