using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Tests.ViewModels
{
    public class ComposerViewModelTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task TestComposerViewModelAsync(int i)
        {
            DatabaseConnection database = new(nameof(TestComposerViewModelAsync));
            await database.ResetTableAsync(ExpectedViewModels.Composers);
            ComposerViewModel viewModel = new(nameof(TestComposerViewModelAsync), true);
            Composer expectedComposer = ExpectedViewModels.Composers[i];
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