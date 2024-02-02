namespace MusicOrganisationApp.Lib.Services
{
    public interface IService
    {
        public const int DEFAULT_LIMIT = 256;
    }

    public interface IService<T> : IService where T : class, new()
    {
        public Task<(bool, T)> TryGetAsync(int id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task InsertAsync(T value, bool getNewId);

        public Task DeleteAsync(T value);

        public Task UpdateAsync(T value);

        public Task<IEnumerable<T>> SearchAsync(string search, string ordering);

        protected static (bool suceeded, T value) TryReturnValue(IEnumerable<T> values)
        {
            if (values.Any())
            {
                return (true, values.First());
            }
            else
            {
                return (false, new());
            }
        }
    }
}