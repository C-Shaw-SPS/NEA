using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : SearchableCollectionViewModel<ComposerData>
    {
        public const string ROUTE = nameof(AllComposersViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(ComposerData.Name),
            ["Year of birth"] = nameof(ComposerData.BirthYear),
            ["Year of death"] = nameof(ComposerData.DeathYear)
        };

        private readonly ComposerService _service;

        public AllComposersViewModel() : base(ComposerViewModel.ROUTE, EditComposerViewModel.ROUTE, _orderings)
        {
            _service = new(_database);
        }

        protected override ISearchService<ComposerData> SearchService => _service;
    }
}