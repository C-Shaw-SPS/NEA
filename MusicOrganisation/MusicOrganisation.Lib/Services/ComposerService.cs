using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Lib.Services
{
    public class ComposerService : IService<ComposerData>
    {
        private readonly Database _database;

        public ComposerService(Database database)
        {
            _database = database;
        }

        public async Task DeleteAsync(ComposerData value)
        {

        }

        public async Task<IEnumerable<ComposerData>> GetAllAsync()
        {
            IEnumerable<ComposerData> composers = await _database.GetAllAsync<ComposerData>();
            return composers;
        }

        public async Task UpdateAsync(ComposerData value)
        {
            await _database.UpdateAsync(value);
        }

        public async Task InitialiseData()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            await _database.InsertAllAsync(composers);
        }
    }
}