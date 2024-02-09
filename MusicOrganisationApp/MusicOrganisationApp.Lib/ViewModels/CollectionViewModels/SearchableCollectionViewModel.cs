using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class SearchableCollectionViewModel<TModel, TModelViewModel, TEditViewModel> : CollectionViewModelBase<TModel, TModelViewModel, TEditViewModel>
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

        protected SearchableCollectionViewModel(Dictionary<string, string> orderings) : base()
        {
            _orderings = orderings;
            _selectedOrdering = Orderings.First();
            _searchCommand = new(SearchAsync);
        }

        protected abstract ISearchService<TModel> SearchService { get; }

        protected override IService<TModel> Service => SearchService;

        public List<string> Orderings => _orderings.Keys.ToList();

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public override async Task RefreshAsync()
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            string ordering = _orderings[SelectedOrdering];
            IEnumerable<TModel> values = await SearchService.SearchAsync(SearchText, ordering);
            IViewModel.ResetCollection(Collection, values);
        }

        async partial void OnSelectedOrderingChanged(string value)
        {
            await SearchAsync();
        }
    }
}