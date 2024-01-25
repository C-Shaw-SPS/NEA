using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class WorkServiceTests
    {
        [Fact]
        public async Task TestInsertWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestInsertWorkAsync));
            
            await database.InsertAllAsync(ExpectedService.ComposerData);

            Work expectedWork = ExpectedService.Works[0];
            await service.InsertAsync(expectedWork, false);
            IEnumerable<Work> actualWorks = await service.GetAllAsync();
            Assert.Single(actualWorks);
            Assert.Contains(expectedWork, actualWorks);

        }

        [Fact]
        public async Task TestGetAllWorksAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestGetAllWorksAsync));

            await database.InsertAllAsync(ExpectedService.ComposerData);
            await database.InsertAllAsync(ExpectedService.WorkData);

            IEnumerable<Work> actualWorks = await service.GetAllAsync();

            Assert.Equal(ExpectedService.Works.Count, actualWorks.Count());

            foreach (Work expectedWork in ExpectedService.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }

        [Fact]
        public async Task TestGetWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestGetWorkAsync));

            await database.InsertAllAsync(ExpectedService.ComposerData);
            await database.InsertAllAsync(ExpectedService.WorkData);

            Work expectedWork = ExpectedService.Works[0];
            (bool suceeded, Work actualWork) = await service.TryGetAsync(expectedWork.Id);

            Assert.True(suceeded);
            Assert.Equal(expectedWork, actualWork);
        }

        [Fact]
        public async Task TestDeleteWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestDeleteWorkAsync));

            await database.ResetTableAsync(ExpectedService.ComposerData);
            await database.ResetTableAsync(ExpectedService.WorkData);
            await database.ResetTableAsync(ExpectedService.RepertoireData);

            Work workToDelete = ExpectedService.Works[0];
            WorkData workDataToDelete = ExpectedService.WorkData[workToDelete.Id];
            IEnumerable<RepertoireData> repertoireDataToDelete = ExpectedService.RepertoireData.Where(r => r.WorkId == workDataToDelete.Id);

            await service.DeleteAsync(workToDelete);

            IEnumerable<WorkData> remainingWorkData = await database.GetAllAsync<WorkData>();
            Assert.DoesNotContain(workDataToDelete, remainingWorkData);

            IEnumerable<RepertoireData> remainingRepertoireData = await database.GetAllAsync<RepertoireData>();
            foreach (RepertoireData repertoire in repertoireDataToDelete)
            {
                Assert.DoesNotContain(repertoire, remainingRepertoireData);
            }
        }

        [Fact]
        public async Task TestSearchWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestSearchWorkAsync));

            await database.InsertAllAsync(ExpectedService.ComposerData);
            await database.ResetTableAsync(ExpectedService.WorkData);

            Work workToSearch = ExpectedService.Works[0];

            foreach (char c in workToSearch.Title)
            {
                IEnumerable<Work> searchResult = await service.SearchAsync(c.ToString(), nameof(Work.Title));
                Assert.Contains(workToSearch, searchResult);
            }
        }

        [Fact]
        public async Task TestUpdateWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestUpdateWorkAsync));

            await database.InsertAllAsync(ExpectedService.ComposerData);
            await database.InsertAllAsync(ExpectedService.WorkData);

            Work originalWork = ExpectedService.Works[0];

            Work updatedWork = new()
            {
                Id = originalWork.Id,
                ComposerId = ExpectedService.ComposerData[1].Id,
                Title = originalWork.Title[1..],
                ComposerName = ExpectedService.ComposerData[1].Name,
            };

            await service.UpdateAsync(updatedWork);

            IEnumerable<Work> updatedWorks = await service.GetAllAsync();

            Assert.Contains(updatedWork, updatedWorks);
            Assert.DoesNotContain(originalWork, updatedWorks);
        }

        private static async Task<(DatabaseConnection database, WorkService service)> GetDatabaseAndWorkServiceAsync(string path)
        {
            DatabaseConnection database = new(path);
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();
            await database.DropTableIfExistsAsync<RepertoireData>();

            WorkService service = new(database);
            return (database, service);
        }
    }
}