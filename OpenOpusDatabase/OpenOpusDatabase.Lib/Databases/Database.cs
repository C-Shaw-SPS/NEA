using SQLite;

namespace OpenOpusDatabase.Lib.Databases
{
    public abstract class Database<T> where T : class, IIdentifiable, new()
    {
        private SQLiteAsyncConnection _connection;
        private readonly string _path;
        private readonly string _tableName;

        public Database(string path)
        {
            _path = FormatDatabasePath(path);
            _tableName = TableNames.Get<T>();
        }

        protected SQLiteAsyncConnection Connection => _connection;

        protected string Path => _path;

        protected string TableName => _tableName;

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

        public abstract Task InsertAsync(T value);

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
            List<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            return result;
        }

        public async Task<T> GetAsync(int id)
        {
            await InitAsync();
            List<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {_tableName} WHERE {nameof(IIdentifiable.Id)} = {id}");
            if (result.Count == 0)
                throw new Exception($"No row in {_tableName} with {nameof(IIdentifiable.Id)} {id}");
            return result[0];
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
            await InitAsync();
            List<T> result = await _connection.QueryAsync<T>($"SELECT {nameof(IIdentifiable.Id)} FROM {_tableName}");
            return result.Select(c => c.Id).ToList();
        }
    }
}