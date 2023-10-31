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


    }
}