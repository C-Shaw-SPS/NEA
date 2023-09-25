using SQLite;
using System.Text;

namespace OpenOpusDatabase.Lib.Databases
{
    public class Database<T> where T : class, ISqlStorable, new()
    {
        private SQLiteAsyncConnection _connection;
        private readonly string _path;
        private readonly string _tableName;

        public Database(string path)
        {
            _path = FormatDatabasePath(path);
            _tableName = TableNames.Get<T>();
        }

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
            string command = $"INSERT INTO {_tableName} {T.GetColumnNames().CommaJoin()} VALUES {value.GetSqlValues().CommaJoin()}";
            await _connection.ExecuteAsync(command);
        }

        public async Task InsertAllAsync(List<T> values)
        {
            await InitAsync();
            InsertCommand<T> insertCommand = new();
            foreach (T value in values)
            {
                insertCommand.AddValue(value);
            }
            await _connection.ExecuteAsync(insertCommand.ToString());
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
            List<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {_tableName} WHERE {nameof(ISqlStorable.Id)} = {id}");
            if (result.Count == 0)
                throw new Exception($"No row in {_tableName} with {nameof(ISqlStorable.Id)} {id}");
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
            List<T> result = await _connection.QueryAsync<T>($"SELECT {nameof(ISqlStorable.Id)} FROM {_tableName}");
            return result.Select(c => c.Id).ToList();
        }
    }
}