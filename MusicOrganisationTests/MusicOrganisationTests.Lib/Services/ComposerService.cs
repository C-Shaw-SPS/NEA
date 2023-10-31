using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Lib.Services
{
    public class ComposerService
    {
        private TableConnection<Composer> _composerTable;

        public ComposerService(string path)
        {
            _composerTable = new(path);
        }

        public async Task InitialiseData()
        {
            IEnumerable<Composer> composers = ComposerGetter.GetFromOpenOpus();
            await _composerTable.InsertAllAsync(composers);
        }

        public async Task AddComposer(string name, string completeName, DateTime birthDate, DateTime deathDate, string era, string? portraitLink = null)
        {
            int id = await _composerTable.GetNextIdAsync();
            Composer composer = new()
            {
                Id = id,
                Name = name,
                CompleteName = completeName,
                BirthDate = birthDate,
                DeathDate = deathDate,
                PortraitLink = portraitLink
            };
            await _composerTable.InsertAsync(composer);
        }
    }
}