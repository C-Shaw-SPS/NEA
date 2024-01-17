using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.ViewModels.EditViewModels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : CollectionViewModelBase<ComposerData>
    {
        public const string ROUTE = nameof(AllComposersViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            { "Name", nameof(ComposerData.Name) },
            { "Year of birth", nameof(ComposerData.BirthYear) },
            { "Year of death", nameof(ComposerData.DeathYear) }
        };

        public AllComposersViewModel() : base(_orderings, ComposerViewModel.ROUTE, nameof(ComposerData.Name))
        {

        }

        protected override async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ComposerViewModel.ID_PARAMETER] = SelectedItem.Id
                };
                await GoToAsync(parameters, ComposerViewModel.ROUTE);
            }
        }

        protected override async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditComposerViewModel.IS_NEW_PARAMETER] = true
            };
            await GoToAsync(parameters, EditComposerViewModel.ROUTE);
        }
    }
}