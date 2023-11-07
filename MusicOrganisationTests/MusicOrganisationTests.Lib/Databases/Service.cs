using SQLite;

namespace MusicOrganisationTests.Lib.Databases
{
    public class Service
    {
        protected SQLiteAsyncConnection _connection;
        private readonly string _path;

        public Service(string path)
        {
            _path = path.FormatAsDatabasePath();
        }

        public async Task InitAsync<T>() where T : class, ITable, new()
        {
            if (_connection is not null)
            {
                return;
            }

            _connection = new SQLiteAsyncConnection(_path, DatabaseProperties.FLAGS);
            await _connection.CreateTableAsync<T>();
        }

        public async Task InsertAsync<T>(T value) where T : class, ITable, new()
        {
            await InitAsync<T>();
            InsertCommand<T> insertCommand = new();
            insertCommand.AddValue(value);
            await _connection.ExecuteAsync(insertCommand.ToString());
            
        }

        public async Task InsertAllAsync<T>(IEnumerable<T> values) where T : class, ITable, new()
        {
            await InitAsync<T>();
            
            InsertCommand<T> insertCommand = new();
            foreach (T value in values)
            {
                insertCommand.AddValue(value);
            }
            await _connection.ExecuteAsync(insertCommand.ToString());
            
        }

        public async Task DeleteAsync<T>(T value) where T : class, ITable, new()
        {
            await InitAsync<T>();
            await _connection.DeleteAsync(value);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, ITable, new()
        {
            await InitAsync<T>();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {T.TableName}");
            return result;
        }

        public async Task<T> GetAsync<T>(int id) where T : class, ITable, new()
        {
            IEnumerable<T> rows = await GetWhereEqualAsync<T>(nameof(ITable.Id), id);
            T? result = rows.FirstOrDefault();
            if (result == null)
            {
                throw new Exception($"No row in {T.TableName} with {nameof(ITable.Id)} {id}");
            }
            else
            {
                return result;
            }
        }

        public async Task<IEnumerable<T>> GetWhereEqualAsync<T>(string propertyName, object value) where T : class, ITable, new()
        {
            await InitAsync<T>();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {T.TableName} WHERE {propertyName} = {SqlFormatting.FormatValue(value)}");
            return result;
        }

        public async Task<IEnumerable<T>> GetWhereTextLikeAsync<T>(string propertyName, string text) where T : class, ITable, new()
        {
            await InitAsync<T>();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT * FROM {T.TableName} WHERE {propertyName} LIKE \"%{text}%\"");
            return result;
        }

        public async Task ClearDataAsync<T>() where T : class, ITable, new()
        {
            await InitAsync<T>();
            await _connection.DeleteAllAsync<T>();
        }

        public async Task UpdateAsync<T>(T value) where T : class, ITable, new()
        {
            await InitAsync<T>();
            await _connection.UpdateAsync(value);
        }

        public async Task<IEnumerable<int>> GetIdsAsync<T>() where T : class, ITable, new()
        {
            await InitAsync<T>();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT {nameof(ITable.Id)} FROM {T.TableName}");
            return result.Select(c => c.Id);
        }

        public async Task<int> GetNextIdAsync<T>() where T : class, ITable, new()
        {
            await InitAsync<T>();
            IEnumerable<T> result = await _connection.QueryAsync<T>($"SELECT Max({nameof(ITable.Id)}) AS {nameof(ITable.Id)} FROM {T.TableName}");
            if (result.Any())
            {
                return result.First().Id + 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query) where T : class, ITable, new()
        {
            await InitAsync<T>();
            return await _connection.QueryAsync<T>(query);
        }
    }
}