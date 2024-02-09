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
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestInsertWorkAsync), false);
            
            Work expectedWork = ExpectedService.Works[0];
            await service.InsertAsync(expectedWork, false);
            IEnumerable<Work> actualWorks = await service.GetAllAsync();
            Assert.Single(actualWorks);
            Assert.Contains(expectedWork, actualWorks);

        }

        [Fact]
        public async Task TestGetAllWorksAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestGetAllWorksAsync), true);

            IEnumerable<Work> actualWorks = await service.GetAllAsync();

            CollectionAssert.Equal(ExpectedService.Works, actualWorks);
        }

        [Fact]
        public async Task TestGetWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestGetWorkAsync), true);

            Work expectedWork = ExpectedService.Works[0];
            (bool suceeded, Work actualWork) = await service.TryGetAsync(expectedWork.Id);

            Assert.True(suceeded);
            Assert.Equal(expectedWork, actualWork);
        }

        [Fact]
        public async Task TestDeleteWorkAsync()
        {
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestDeleteWorkAsync), true);

            await database.ResetTableAsync(ExpectedService.RepertoireDatas);

            Work workToDelete = ExpectedService.Works[0];
            WorkData workDataToDelete = ExpectedService.WorkDatas[workToDelete.Id];
            IEnumerable<RepertoireData> repertoireDataToDelete = ExpectedService.RepertoireDatas.Where(r => r.WorkId == workDataToDelete.Id);

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
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestSearchWorkAsync), true);

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
            (DatabaseConnection database, WorkService service) = await GetDatabaseAndWorkServiceAsync(nameof(TestUpdateWorkAsync), true);

            Work originalWork = ExpectedService.Works[0];

            Work updatedWork = new()
            {
                Id = originalWork.Id,
                ComposerId = ExpectedService.Composers[1].Id,
                Title = originalWork.Title[1..],
                ComposerName = ExpectedService.Composers[1].Name,
            };

            await service.UpdateAsync(updatedWork);

            IEnumerable<Work> updatedWorks = await service.GetAllAsync();

            Assert.Contains(updatedWork, updatedWorks);
            Assert.DoesNotContain(originalWork, updatedWorks);
        }

        private static async Task<(DatabaseConnection database, WorkService service)> GetDatabaseAndWorkServiceAsync(string path, bool insertWorkData)
        {
            DatabaseConnection database = new(path);
            await database.DropTableIfExistsAsync<Composer>();
            await database.DropTableIfExistsAsync<WorkData>();
            await database.DropTableIfExistsAsync<RepertoireData>();

            await database.InsertAllAsync(ExpectedService.Composers);

            if (insertWorkData)
            {
                await database.InsertAllAsync(ExpectedService.WorkDatas);
            }

            WorkService service = new(database);
            return (database, service);
        }
    }
}