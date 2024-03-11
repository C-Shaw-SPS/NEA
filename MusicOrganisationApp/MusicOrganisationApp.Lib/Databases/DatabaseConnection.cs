using SQLite;

namespace MusicOrganisationApp.Lib.Databases
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _path;
        private readonly SQLiteAsyncConnection _connection;

        public DatabaseConnection(string path)
        {
            _path = path.FormatAsDatabasePath();
            _connection = new(_path, DatabaseProperties.FLAGS);
        }

        public async Task CreateTableAsync<T>() where T : class, ITable, new()
        {
            await _connection.CreateTableAsync<T>();
        }

        public async Task CreateTablesAsync(IEnumerable<Type> types)
        {
            Type[] typeArray = [.. types];
            await _connection.CreateTablesAsync(CreateFlags.None, typeArray);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(ISqlQuery sqlQuery) where T : class, new()
        {
            await CreateTablesAsync(sqlQuery.Tables);
            string sql = sqlQuery.GetSql();
            IEnumerable<T> result = await QueryAsync<T>(sql);
            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery) where T : class, new()
        {
            IEnumerable<T> result = await _connection.QueryAsync<T>(sqlQuery);
            return result;
        }

        public async Task ExecuteAsync<T>(ISqlStatement<T> sqlStatement, bool init) where T : class, ITable, new()
        {
            if (init)
            {
                await CreateTableAsync<T>();
            }

            string sql = sqlStatement.GetSql();
            await _connection.ExecuteAsync(sql);
        }

        public async Task InsertAsync<T>(T value, bool getNewId) where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            if (getNewId)
            {
                value.Id = await GetNextIdAsync<T>();
            }

            InsertStatement<T> insertStatement = new();
            insertStatement.AddValue(value);
            await ExecuteAsync(insertStatement, true);
        }

        public async Task InsertAllAsync<T>(IEnumerable<T> values, bool getNewIds) where T : class, ITable, new()
        {
            if (values.Any())
            {
                InsertStatement<T> insertStatement = new();
                int id = getNewIds ? await GetNextIdAsync<T>() : 0;
                foreach (T value in values)
                {
                    if (getNewIds)
                    {
                        value.Id = id;
                        ++id;
                    }
                    insertStatement.AddValue(value);
                }
                await ExecuteAsync(insertStatement, true);
            }
        }

        public async Task ClearTableAsync<T>() where T : class, ITable, new()
        {
            DeleteStatement<T> deleteStatement = new();
            await ExecuteAsync(deleteStatement, true);
        }

        public async Task<(bool found, T value)> TryGetAsync<T>(int id) where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            SqlQuery<T> sqlQuery = new() { SelectAll = true };
            sqlQuery.AddWhereEqual<T>(nameof(ITable.Id), id);
            IEnumerable<T> result = await QueryAsync<T>(sqlQuery);
            if (result.Any())
            {
                T value = result.First();
                return (true, value);
            }
            else
            {
                return (false, new());
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            SqlQuery<T> sqlQuery = new() { SelectAll = true };
            IEnumerable<T> result = await QueryAsync<T>(sqlQuery);
            return result;
        }

        public async Task DeleteAsync<T>(T value) where T : class, ITable, new()
        {
            await DeleteAsync<T>(value.Id);
        }

        public async Task DeleteAsync<T>(int id) where T : class, ITable, new()
        {
            DeleteStatement<T> deleteStatement = new();
            deleteStatement.AddWhereEqual<T>(nameof(ITable.Id), id);
            await ExecuteAsync(deleteStatement, true);
        }

        public async Task UpdateAsync<T>(T value) where T : class, ITable, new()
        {
            UpdateStatement<T> updateStatement = new(value);
            await ExecuteAsync(updateStatement, true);
        }

        public async Task DropTableIfExistsAsync<T>() where T : class, ITable, new()
        {
            DropTableStatement<T> dropTableStatement = new();
            await ExecuteAsync(dropTableStatement, init: false);
        }

        private async Task<int> GetNextIdAsync<T>() where T : class, ITable, new()
        {
            await CreateTableAsync<T>();
            string query = $"SELECT MAX({nameof(ITable.Id)}) + 1 AS {nameof(ITable.Id)} FROM {T.TableName}";
            IEnumerable<T> result = await _connection.QueryAsync<T>(query);
            int nextId = result.First().Id;
            return nextId;
        }

        public async Task ResetTableAsync<T>(IEnumerable<T> values, bool getNewIds) where T : class, ITable, new()
        {
            await DropTableIfExistsAsync<T>();
            await InsertAllAsync(values, getNewIds);
        }

        public async Task<int> GetTableCount<T>() where T : class, ITable, new()
        {
            await CreateTableAsync<T>();
            string query = $"SELECT COUNT(*) AS {nameof(CountProperty.Count)} FROM {T.TableName}";
            IEnumerable<CountProperty> countProperties = await _connection.QueryAsync<CountProperty>(query);
            CountProperty countProperty = countProperties.First();
            return countProperty.Count;
        }

        public async Task<bool> IsEmptyTable<T>() where T : class, ITable, new()
        {
            int tableCount = await GetTableCount<T>();
            return tableCount == 0;
        }
    }
}