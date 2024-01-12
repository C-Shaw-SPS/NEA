using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Json;

namespace MusicOrganisation.Lib.Services
{
    public class ComposerService : Service
    {

        public ComposerService(string path) : base(path)
        {

        }

        public async Task InitialiseData()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            await InsertAllAsync(composers);
        }

        public async Task InsertComposerAsync(string name, int? birthDate, int? deathDate, string era)
        {
            int id = await GetNextIdAsync<ComposerData>();
            ComposerData composer = new()
            {
                Id = id,
                Name = name,
                BirthYear = birthDate,
                DeathYear = deathDate,
                Era = era,
            };
            await InsertAsync(composer);
        }
    }
}