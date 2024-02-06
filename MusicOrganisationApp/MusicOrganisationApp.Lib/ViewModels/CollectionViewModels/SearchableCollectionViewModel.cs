using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class SearchableCollectionViewModel<T> : CollectionViewModelBase<T> where T : class, IIdentifiable, new()
    {

        private readonly Dictionary<string, string> _orderings;
        private readonly AsyncRelayCommand _searchCommand;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedOrdering;

        protected SearchableCollectionViewModel(string modelRoute, string editRoute, Dictionary<string, string> orderings) : base(modelRoute, editRoute)
        {
            _orderings = orderings;
            _selectedOrdering = Orderings.First();
            _searchCommand = new(SearchAsync);
        }

        protected abstract ISearchService<T> SearchService { get; }

        protected override IService<T> Service => SearchService;

        public List<string> Orderings => _orderings.Keys.ToList();

        public AsyncRelayCommand SearchCommand => _searchCommand;

        private async Task SearchAsync()
        {
            string ordering = _orderings[SelectedOrdering];
            IEnumerable<T> values = await SearchService.SearchAsync(SearchText, ordering);
            ResetCollection(Collection, values);
        }

        async partial void OnSelectedOrderingChanged(string value)
        {
            await SearchAsync();
        }
    }
}