using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.Tests.ViewModels
{
    public class AllComposersViewModelTests
    {
        [Fact]
        public async Task TestEmptySearchAsync()
        {
            DatabaseConnection database = new(nameof(TestEmptySearchAsync));
            await database.ResetTableAsync(ExpectedViewModels.Composers, false);
            AllComposersViewModel viewModel = new(nameof(TestEmptySearchAsync), true)
            {
                SearchText = string.Empty
            };
            await viewModel.SearchCommand.ExecuteAsync(null);
            CollectionAssert.EqualContents(ExpectedViewModels.Composers, viewModel.Collection);
        }

        [Fact]
        public async Task TestSearchAsync()
        {
            DatabaseConnection database = new(nameof(TestSearchAsync));
            await database.ResetTableAsync(ExpectedViewModels.Composers, false);
            AllComposersViewModel viewModel = new(nameof(TestSearchAsync), true);
            foreach (Composer expectedComposer in ExpectedViewModels.Composers)
            {
                await viewModel.SearchCommand.ExecuteAsync(null);
                Assert.Contains(expectedComposer, viewModel.Collection);
            }
        }
    }
}