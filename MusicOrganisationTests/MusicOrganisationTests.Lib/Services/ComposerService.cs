using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Lib.Services
{
    public class ComposerService : Service<Composer>
    {



        public ComposerService(string path) : base(path)
        {

        }

        public async Task InitialiseData()
        {
            IEnumerable<Composer> composers = ComposerGetter.GetFromOpenOpus();
            await InsertAllAsync(composers);
        }

        public async Task InsertAsync(string name, string completeName, DateTime birthDate, DateTime? deathDate, string era, string? portraitLink = null)
        {
            int id = await GetNextIdAsync();
            Composer composer = new()
            {
                Id = id,
                Name = name,
                CompleteName = completeName,
                BirthDate = birthDate,
                DeathDate = deathDate,
                Era = era,
                PortraitLink = portraitLink
            };
            await InsertAsync(composer);
        }
    }
}