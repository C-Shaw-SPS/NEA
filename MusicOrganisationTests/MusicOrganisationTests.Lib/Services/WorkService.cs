using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Databases;

namespace MusicOrganisationTests.Lib.Services
{
    public class WorkService : Service<Work>
    {
        public WorkService(string path) : base(path) { }

        public async Task InitialiseData()
        {
            IEnumerable<Work> works = WorkGetter.GetFromOpenOpus();
            await InsertAllAsync(works);
        }

        public async Task Add(int composerId, string title, string subtitle, string genre)
        {
            int id = await GetNextIdAsync();
            Work work = new()
            {
                Id = id,
                ComposerId = composerId,
                Title = title,
                Subtitle = subtitle,
                Genre = genre
            };
            await InsertAsync(work);
        }

        public async Task<IEnumerable<Work>> GetFromComposer(Composer composer)
        {
            return await GetWhereEqualAsync(nameof(Work.ComposerId), composer.Id);
        }
    }
}