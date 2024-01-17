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

        public AllComposersViewModel() : base(_orderings, ComposerViewModel.ROUTE, EditComposerViewModel.ROUTE, nameof(ComposerData.Name))
        {

        }
    }
}