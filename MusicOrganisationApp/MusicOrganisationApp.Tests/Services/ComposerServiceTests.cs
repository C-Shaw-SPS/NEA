using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using Xunit.Sdk;

namespace MusicOrganisationApp.Tests.Services
{
    public class ComposerServiceTests
    {
        [Fact]
        public async Task TestInsertComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestInsertComposerAsync));

            await service.InsertAsync(ExpectedService.ComposerDatas[0], false);

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync();

            Assert.Single(actualComposers);
            Assert.Contains(ExpectedService.ComposerDatas[0], actualComposers);
        }

        [Fact]
        public async Task TestDeleteComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestDeleteComposerAsync));

            await database.InsertAllAsync(ExpectedService.ComposerDatas);
            await database.InsertAllAsync(ExpectedService.WorkDatas);
            await database.InsertAllAsync(ExpectedService.RepertoireDatas);

            ComposerData composerToDelete = ExpectedService.ComposerDatas[0];
            IEnumerable<WorkData> worksToDelete = ExpectedService.WorkDatas.Where(work => work.ComposerId == composerToDelete.Id);
            IEnumerable<RepertoireData> repertoireToDelete = ExpectedService.RepertoireDatas.Where(repetroire => worksToDelete.Any(work => work.Id == repetroire.WorkId));

            await service.DeleteAsync(composerToDelete);

            IEnumerable<ComposerData> remainingComposers = await database.GetAllAsync<ComposerData>();
            IEnumerable<WorkData> remainingWorks = await database.GetAllAsync<WorkData>();
            IEnumerable<RepertoireData> remainingRepertoire = await database.GetAllAsync<RepertoireData>();

            Assert.DoesNotContain(composerToDelete, remainingComposers);
            foreach (WorkData deletedWork in worksToDelete)
            {
                Assert.DoesNotContain(deletedWork, remainingWorks);
            }
            foreach (RepertoireData deletedRepertoire in repertoireToDelete)
            {
                Assert.DoesNotContain(deletedRepertoire, remainingRepertoire);
            }
        }

        [Fact]
        public async Task TestUpdateComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestUpdateComposerAsync));

            ComposerData originalComposer = ExpectedService.ComposerDatas[0];

            await database.InsertAsync(originalComposer);

            ComposerData updatedComposer = new()
            {
                Id = originalComposer.Id,
                Name = "Updated name"
            };

            await service.UpdateAsync(updatedComposer);

            IEnumerable<ComposerData> updatedComposers = await service.GetAllAsync();
            Assert.Single(updatedComposers);
            Assert.Contains(updatedComposer, updatedComposers);
            Assert.DoesNotContain(originalComposer, updatedComposers);
        }

        [Fact]
        public async Task TestSearchComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestSearchComposerAsync));
            await database.InsertAllAsync(ExpectedService.ComposerDatas);
            ComposerData composerToSearch = ExpectedService.ComposerDatas[0];
            foreach (char c in composerToSearch.Name)
            {
                IEnumerable<ComposerData> searchResult = await service.SearchAsync(c.ToString(), nameof(ComposerData.Name));
                Assert.Contains(composerToSearch, searchResult);
            }
        }

        [Fact]
        public async Task TestGetComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestGetComposerAsync));
            await database.InsertAllAsync(ExpectedService.ComposerDatas);
            ComposerData expectedComposer = ExpectedService.ComposerDatas[0];
            (bool suceeded, ComposerData actualComposer) = await service.TryGetAsync(expectedComposer.Id);
            Assert.True(suceeded);
            Assert.Equal(expectedComposer, actualComposer);
        }

        private static async Task<(DatabaseConnection database, ComposerService service)> GetDatabaseAndServiceAsync(string path)
        {
            DatabaseConnection database = new(path);
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();
            await database.DropTableIfExistsAsync<RepertoireData>();
            ComposerService service = new(database);
            return (database, service);
        }
    }
}