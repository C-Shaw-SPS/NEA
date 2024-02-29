using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : SearchableCollectionViewModel<Composer, ComposerViewModel, EditComposerViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllComposersViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(Composer.Name),
            ["Year of birth"] = nameof(Composer.BirthYear),
            ["Year of death"] = nameof(Composer.DeathYear)
        };

        private readonly ComposerService _service;

        public AllComposersViewModel() : base(_orderings)
        {
            _service = new(_database);
        }

        public AllComposersViewModel(string path, bool isTesting) : base(_orderings, path, isTesting)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<Composer> SearchService => _service;
    }
}