
namespace MusicOrganisationApp.Lib.Databases
{
    public interface IDatabaseConnection
    {
        Task ClearTableAsync<T>() where T : class, ITable, new();
        Task CreateTableAsync<T>() where T : class, ITable, new();
        Task CreateTablesAsync(IEnumerable<Type> types);
        Task DeleteAsync<T>(int id) where T : class, ITable, new();
        Task DeleteAsync<T>(T value) where T : class, ITable, new();
        Task DropTableIfExistsAsync<T>() where T : class, ITable, new();
        Task ExecuteAsync<T>(ISqlStatement<T> sqlStatement, bool init) where T : class, ITable, new();
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class, ITable, new();
        //Task<int> GetNextIdAsync<T>() where T : class, ITable, new();
        Task<int> GetTableCount<T>() where T : class, ITable, new();
        Task InsertAllAsync<T>(IEnumerable<T> values, bool getNewIds) where T : class, ITable, new();
        Task InsertAsync<T>(T value, bool getNewId) where T : class, ITable, new();
        Task<bool> IsEmptyTable<T>() where T : class, ITable, new();
        Task<IEnumerable<T>> QueryAsync<T>(ISqlQuery sqlQuery) where T : class, new();
        Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery) where T : class, new();
        Task ResetTableAsync<T>(IEnumerable<T> values, bool getNewIds) where T : class, ITable, new();
        Task<(bool found, T value)> TryGetAsync<T>(int id) where T : class, ITable, new();
        Task UpdateAsync<T>(T value) where T : class, ITable, new();
    }
}