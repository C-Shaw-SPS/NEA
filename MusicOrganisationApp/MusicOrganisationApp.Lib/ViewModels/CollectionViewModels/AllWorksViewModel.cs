using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllWorksViewModel : SearchableCollectionViewModel<Work, WorkViewModel, EditWorkViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllWorksViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Title"] = nameof(Work.Title),
            ["Composer name"] = nameof(Work.ComposerName)
        };

        private readonly WorkService _service;

        public AllWorksViewModel() : base(_orderings)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<Work> SearchService => _service;
    }
}