using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllWorksViewModel : SearchableCollectionViewModel<Work>
    {
        public const string ROUTE = nameof(AllWorksViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Title"] = nameof(Work.Title),
            ["Composer name"] = nameof(Work.ComposerName)
        };

        private readonly WorkService _service;

        public AllWorksViewModel() : base(WorkViewModel.ROUTE, EditWorkViewModel.ROUTE, _orderings)
        {
            _service = new(_database);
        }

        protected override ISearchService<Work> SearchService => _service;
    }
}