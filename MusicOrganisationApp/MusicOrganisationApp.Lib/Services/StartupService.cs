using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class StartupService
    {
        private readonly DatabaseConnection _database;

        public StartupService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task InitialiseComposersAndWorks()
        {
            await InitialiseComposers();
            await InitialiseWorks();
        }

        private async Task InitialiseComposers()
        {
            bool isEmpty = await _database.IsEmptyTable<ComposerData>();
            if (isEmpty)
            {
                IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
                await _database.InsertAllAsync(composers);
            }
        }

        private async Task InitialiseWorks()
        {
            bool isEmpty = await _database.IsEmptyTable<WorkData>();
            if (isEmpty)
            {
                IEnumerable<WorkData> works = await WorkGetter.GetFromOpenOpus();
                await _database.InsertAllAsync(works);
            }
        }
    }
}