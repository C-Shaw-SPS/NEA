using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Lib.Services
{
    public class ComposerService : IService<ComposerData>
    {
        private readonly DatabaseConnection _database;

        public ComposerService(DatabaseConnection database)
        {
            _database = database;
        }

        public async Task DeleteAsync(ComposerData value)
        {
            IEnumerable<WorkData> works = await GetWorksAsync(value);

            DeleteQuery<WorkData> deleteWorkDataQuery = new();
            deleteWorkDataQuery.AddCondition(nameof(WorkData.ComposerId), value.Id);

            DeleteQuery<RepertoireData> deleteRepertoireQuery = new();
            foreach (WorkData work in works)
            {
                deleteRepertoireQuery.AddCondition(nameof(RepertoireData.WorkId), work.Id);
            }

            await _database.DeleteAsync(value);
            await _database.ExecuteAsync(deleteWorkDataQuery);
            await _database.ExecuteAsync(deleteRepertoireQuery);
        }

        private async Task<IEnumerable<WorkData>> GetWorksAsync(ComposerData composer)
        {
            SelectQuery<WorkData> selectQuery = new();
            selectQuery.SetSelectAll();
            selectQuery.AddWhereEquals<WorkData>(nameof(WorkData.ComposerId), composer.Id);
            IEnumerable<WorkData> works = await _database.QueryAsync<WorkData>(selectQuery);
            return works;
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