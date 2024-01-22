namespace MusicOrganisationApp.Lib.Services
{
    public interface IService<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task InsertAsync(T value);

        public Task DeleteAsync(T value);

        public Task UpdateAsync(T value);

        public Task<IEnumerable<T>> SearchAsync(string search, string ordering);
    }
}