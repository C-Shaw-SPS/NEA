using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Lib.Services
{
    public class WorkService : Service<Work>
    {
        public WorkService(string path) : base(path) { }

        public async Task InitialiseData()
        {
            IEnumerable<Work> works = WorkGetter.GetFromOpenOpus();
            await _table.InsertAllAsync(works);
        }

        public async Task Add(int composerId, string title, string subtitle, string genre)
        {
            int id = await _table.GetNextIdAsync();
            Work work = new()
            {
                Id = id,
                ComposerId = composerId,
                Title = title,
                Subtitle = subtitle,
                Genre = genre
            };
            await _table.InsertAsync(work);
        }

        public async Task<IEnumerable<Work>> GetFromComposer(Composer composer)
        {
            return await _table.GetWhereEqualAsync(nameof(Work.ComposerId), composer.Id);
        }
    }
}