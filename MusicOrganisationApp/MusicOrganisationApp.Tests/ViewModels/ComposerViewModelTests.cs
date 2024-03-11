using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Tests.ViewModels
{
    public class ComposerViewModelTests
    {
        [Fact]
        public async Task TestComposerViewModelAsync()
        {
            DatabaseConnection database = new(nameof(TestComposerViewModelAsync));
            await database.ResetTableAsync(ExpectedViewModels.Composers, false);
            ComposerViewModel viewModel = new(nameof(TestComposerViewModelAsync), true);
            foreach (Composer expectedComposer in ExpectedViewModels.Composers)
            {
                Dictionary<string, object> query = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = expectedComposer.Id
                };
                await viewModel.ApplyQueryAttributesAsync(query);
                Assert.Equal(expectedComposer.Name, viewModel.Name);
                Assert.Equal(expectedComposer.BirthYear.ToString(), viewModel.BirthYear);
                Assert.Equal(expectedComposer.DeathYear.ToString(), viewModel.DeathYear);
                Assert.Equal(expectedComposer.Era, viewModel.Era);
            }
        }
    }
}