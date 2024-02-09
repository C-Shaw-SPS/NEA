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
            bool isEmpty = await IsEmptyComposerAndWorkTables();
            if (isEmpty)
            {
                await InitialiseComposers();
                await InitialiseWorks();
            }
        }

        private async Task<bool> IsEmptyComposerAndWorkTables()
        {
            bool isEmpty = true;
            isEmpty &= await _database.IsEmptyTable<Composer>();
            isEmpty &= await _database.IsEmptyTable<WorkData>();
            return isEmpty;
        }

        private async Task InitialiseComposers()
        {
            IEnumerable<Composer> composers = await ComposerGetter.GetFromOpenOpus();
            await _database.InsertAllAsync(composers);
        }

        private async Task InitialiseWorks()
        {
            IEnumerable<WorkData> works = await WorkGetter.GetFromOpenOpus();
            await _database.InsertAllAsync(works);
        }
    }
}