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

        public async Task CreateTableAsync<T>() where T : class, ITable, new()
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

        public async Task InsertAsync<T>(T value) where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            InsertStatement<T> insertStatement = new();
            insertStatement.AddValue(value);
            await ExecuteAsync(insertStatement);
        }

        public async Task InsertAllAsync<T>(IEnumerable<T> values) where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            InsertStatement<T> insertStatement = new();
            foreach (T value in values)
            {
                insertStatement.AddValue(value);
            }
            await ExecuteAsync(insertStatement);
        }

        public async Task ClearTableAsync<T>() where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            DeleteStatement<T> deleteStatement = new();
            await ExecuteAsync(deleteStatement);
        }

        public async Task<(bool found, T value)> TryGetAsync<T>(int id) where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            SqlQuery<T> sqlQuery = new();
            sqlQuery.SetSelectAll();
            sqlQuery.AddWhereEquals<T>(nameof(ITable.Id), id);
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

            SqlQuery<T> sqlQuery = new();
            sqlQuery.SetSelectAll();
            IEnumerable<T> result = await QueryAsync<T>(sqlQuery);
            return result;
        }

        public async Task DropTableAsync<T>() where T : class, ITable, new()
        {
            await CreateTableAsync<T>();

            DropTableStatement<T> dropTableStatement = new();
            await ExecuteAsync(dropTableStatement);
        }
    }
}