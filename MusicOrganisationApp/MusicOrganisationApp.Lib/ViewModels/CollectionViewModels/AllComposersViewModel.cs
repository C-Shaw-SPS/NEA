using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : SearchableCollectionViewModel<ComposerData, ComposerViewModel, EditComposerViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllComposersViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(ComposerData.Name),
            ["Year of birth"] = nameof(ComposerData.BirthYear),
            ["Year of death"] = nameof(ComposerData.DeathYear)
        };

        private readonly ComposerService _service;

        public AllComposersViewModel() : base(_orderings)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<ComposerData> SearchService => _service;
    }
}