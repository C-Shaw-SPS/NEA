using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Lib.Services
{
    public class ComposerService : Service
    {

        public ComposerService(string path) : base(path)
        {

        }

        public async Task InitialiseData()
        {
            IEnumerable<Composer> composers = ComposerGetter.GetFromOpenOpus();
            await InsertAllAsync(composers);
        }

        public async Task InsertComposerAsync(string name, string completeName, DateTime birthDate, DateTime? deathDate, string era, string? portraitLink = null)
        {
            int id = await GetNextIdAsync<Composer>();
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