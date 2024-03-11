using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Tests.ViewModels
{
    public class EditComposerViewModelTests
    {
        [Fact]
        public async Task TestValidSaveAsync()
        {
            EditComposerViewModel viewModel = new(nameof(TestValidSaveAsync), true)
            {
                Name = "Bach",
                BirthYear = "1685",
                DeathYear = "1750"
            };
            await viewModel.TrySaveCommand.ExecuteAsync(null);
            Assert.False(viewModel.IsCurrentViewModel);
            Assert.Empty(viewModel.NameError);
            Assert.Empty(viewModel.BirthYearError);
            Assert.Empty(viewModel.DeathYearError);
        }

        [Fact]
        public async Task TestSaveWithInvalidNameAsync()
        {
            EditComposerViewModel viewModel = new(nameof(TestSaveWithInvalidNameAsync), true);
            await viewModel.TrySaveCommand.ExecuteAsync(null);
            Assert.True(viewModel.IsCurrentViewModel);
            Assert.NotEmpty(viewModel.NameError);
        }

        [Fact]
        public async Task TestSaveWithInvalidDatesAsync()
        {
            EditComposerViewModel viewModel = new(nameof(TestSaveWithInvalidDatesAsync), true)
            {
                Name = "Bach",
                BirthYear = "1750",
                DeathYear = "1685"
            };
            await viewModel.TrySaveCommand.ExecuteAsync(null);
            Assert.True(viewModel.IsCurrentViewModel);
            Assert.NotEmpty(viewModel.DeathYearError);
        }

        [Fact]
        public void TestNumericDateValidation()
        {
            string validBirthYear = "2024";
            string invalidBirthYear = "hello world!";

            EditComposerViewModel viewModel = new(nameof(TestNumericDateValidation), true)
            {
                BirthYear = validBirthYear
            };
            Assert.Equal(validBirthYear, viewModel.BirthYear);
            viewModel.BirthYear = invalidBirthYear;
            Assert.Equal(validBirthYear, viewModel.BirthYear);
        }

        [Fact]
        public async Task TestEditExistingComposerAsync()
        {
            DatabaseConnection database = new(nameof(TestEditExistingComposerAsync));
            await database.ResetTableAsync(ExpectedViewModels.Composers, false);
            EditComposerViewModel viewModel = new(nameof(TestEditExistingComposerAsync), true);
            foreach (Composer expectedComposer in ExpectedViewModels.Composers)
            {
                Dictionary<string, object> query = new()
                {
                    [EditViewModelBase.ID_PARAMETER] = expectedComposer.Id
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