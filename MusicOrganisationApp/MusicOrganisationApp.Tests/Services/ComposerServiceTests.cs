using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Services
{
    public class ComposerServiceTests
    {
        [Fact]
        public async Task TestAddComposerAsync()
        {
            DatabaseConnection database = new(nameof(TestAddComposerAsync));
            await database.DropTableIfExistsAsync<ComposerData>();

            ComposerService composerService = new(database);
            await composerService.InsertAsync(ExpectedComposerService.Composers[0], false);

            IEnumerable<ComposerData> actualComposers = await composerService.GetAllAsync();

            Assert.Single(actualComposers);
            Assert.Contains(ExpectedComposerService.Composers[0], actualComposers);
        }

        [Fact]
        public async Task TestDeleteComposerAsync()
        {
            DatabaseConnection database = new(nameof(TestAddComposerAsync));
            await database.DropTableIfExistsAsync<ComposerData>();
            await database.DropTableIfExistsAsync<WorkData>();
            await database.DropTableIfExistsAsync<RepertoireData>();

            await database.InsertAllAsync(ExpectedComposerService.Composers);
            await database.InsertAllAsync(ExpectedComposerService.Works);
            await database.InsertAllAsync(ExpectedComposerService.Repertoires);

            ComposerData composerToDelete = ExpectedComposerService.Composers[0];
            IEnumerable<WorkData> worksToDelete = ExpectedComposerService.Works.Where(work => work.ComposerId == composerToDelete.Id);
            IEnumerable<RepertoireData> repertoireToDelete = ExpectedComposerService.Repertoires.Where(repetroire => worksToDelete.Any(work => work.Id == repetroire.WorkId));

            ComposerService composerService = new(database);
            await composerService.DeleteAsync(composerToDelete);

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
    }
}