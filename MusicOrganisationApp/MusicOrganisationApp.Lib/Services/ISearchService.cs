using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Lib.Services
{
    public interface ISearchService<T> : IService<T> where T : class, IIdentifiable, new()
    {
        public Task<IEnumerable<T>> SearchAsync(string search, string ordering);
    }
}