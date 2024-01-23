using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.IndividualViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(ComposerData.Name),
            ["Year of birth"] = nameof(ComposerData.BirthYear),
            ["Year of death"] = nameof(ComposerData.DeathYear)
        };

        private readonly ComposerService _service;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _selectedOrdering;

        [ObservableProperty]
        private ObservableCollection<ComposerData> _collection;

        [ObservableProperty]
        private ComposerData? _selectedItem;

        private readonly AsyncRelayCommand _addNewCommand;
        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;

        public AllComposersViewModel()
        {
            _service = new(_database);

            _searchText = string.Empty;
            _selectedOrdering = _orderings.Keys.First();
            _collection = [];
            _selectedItem = null;

            _addNewCommand = new(AddNewAsync);
            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);
        }

        public static List<string> Orderings => _orderings.Keys.ToList();

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public async Task RefreshAsync()
        {
            await SearchAsync();
        }

        private async Task AddNewAsync()
        {
            throw new NotImplementedException();
        }

        private async Task SearchAsync()
        {
            string ordering = _orderings[SelectedOrdering];
            IEnumerable<ComposerData> composers = await _service.SearchAsync(SearchText, ordering);
            Collection.Clear();
            foreach (ComposerData composer in composers)
            {
                Collection.Add(composer);
            }
        }

        private async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ComposerViewModel.ID_PARAMETER] = SelectedItem.Id
                };
                await GoToAsync(parameters, ComposerViewModel.Route);
            }
        }
    }
}