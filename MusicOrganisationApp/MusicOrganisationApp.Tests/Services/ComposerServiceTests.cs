using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class ComposerServiceTests
    {
        [Fact]
        public async Task TestInsertComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestInsertComposerAsync));

            await service.InsertAsync(ExpectedService.Composers[0], false);

            IEnumerable<Composer> actualComposers = await service.GetAllAsync();

            Assert.Single(actualComposers);
            Assert.Contains(ExpectedService.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestDeleteComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestDeleteComposerAsync));

            await database.InsertAllAsync(ExpectedService.Composers);
            await database.InsertAllAsync(ExpectedService.WorkDatas);
            await database.InsertAllAsync(ExpectedService.RepertoireDatas);

            Composer composerToDelete = ExpectedService.Composers[0];
            IEnumerable<WorkData> worksToDelete = ExpectedService.WorkDatas.Where(work => work.ComposerId == composerToDelete.Id);
            IEnumerable<RepertoireData> repertoireToDelete = ExpectedService.RepertoireDatas.Where(repetroire => worksToDelete.Any(work => work.Id == repetroire.WorkId));

            await service.DeleteAsync(composerToDelete);

            IEnumerable<Composer> remainingComposers = await database.GetAllAsync<Composer>();
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

            Composer originalComposer = ExpectedService.Composers[0];

            await database.InsertAsync(originalComposer);

            Composer updatedComposer = new()
            {
                Id = originalComposer.Id,
                Name = "Updated name"
            };

            await service.UpdateAsync(updatedComposer);

            IEnumerable<Composer> updatedComposers = await service.GetAllAsync();
            Assert.Single(updatedComposers);
            Assert.Contains(updatedComposer, updatedComposers);
            Assert.DoesNotContain(originalComposer, updatedComposers);
        }

        [Fact]
        public async Task TestSearchComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestSearchComposerAsync));
            await database.InsertAllAsync(ExpectedService.Composers);
            Composer composerToSearch = ExpectedService.Composers[0];
            foreach (char c in composerToSearch.Name)
            {
                IEnumerable<Composer> searchResult = await service.SearchAsync(c.ToString(), nameof(Composer.Name));
                Assert.Contains(composerToSearch, searchResult);
            }
        }

        [Fact]
        public async Task TestGetComposerAsync()
        {
            (DatabaseConnection database, ComposerService service) = await GetDatabaseAndServiceAsync(nameof(TestGetComposerAsync));
            await database.InsertAllAsync(ExpectedService.Composers);
            Composer expectedComposer = ExpectedService.Composers[0];
            (bool suceeded, Composer actualComposer) = await service.TryGetAsync(expectedComposer.Id);
            Assert.True(suceeded);
            Assert.Equal(expectedComposer, actualComposer);
        }

        private static async Task<(DatabaseConnection database, ComposerService service)> GetDatabaseAndServiceAsync(string path)
        {
            DatabaseConnection database = new(path);
            await database.DropTableIfExistsAsync<Composer>();
            await database.DropTableIfExistsAsync<WorkData>();
            await database.DropTableIfExistsAsync<RepertoireData>();
            ComposerService service = new(database);
            return (database, service);
        }
    }
}