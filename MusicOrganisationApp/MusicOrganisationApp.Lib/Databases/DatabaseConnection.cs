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
    }
}