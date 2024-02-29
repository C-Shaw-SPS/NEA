using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class SearchableCollectionViewModel<TModel, TModelViewModel, TEditViewModel> : SelectableCollectionViewModel<TModel, TModelViewModel, TEditViewModel>
        where TModel : class, IIdentifiable, new()
        where TModelViewModel : IViewModel
        where TEditViewModel : IViewModel
    {

        private readonly Dictionary<string, string> _orderings;
        private readonly AsyncRelayCommand _searchCommand;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedOrdering;

        public SearchableCollectionViewModel(Dictionary<string, string> orderings) : base()
        {
            _orderings = orderings;
            _selectedOrdering = Orderings.First();
            _searchCommand = new(RefreshAsync);
        }

        public SearchableCollectionViewModel(Dictionary<string, string> orderings, string path, bool isTesting) : base(path, isTesting)
        {
            _orderings = orderings;
            _selectedOrdering = Orderings.First();
            _searchCommand = new(RefreshAsync);
        }

        protected abstract ISearchService<TModel> SearchService { get; }

        protected override IService<TModel> Service => SearchService;

        public List<string> Orderings => _orderings.Keys.ToList();

        public AsyncRelayCommand SearchCommand => _searchCommand;

        protected override async Task<IEnumerable<TModel>> GetAllAsync()
        {
            string ordering = _orderings[SelectedOrdering];
            IEnumerable<TModel> searchResult = await SearchService.SearchAsync(SearchText, ordering);
            return searchResult;
        }

        async partial void OnSelectedOrderingChanged(string value)
        {
            await RefreshAsync();
        }
    }
}