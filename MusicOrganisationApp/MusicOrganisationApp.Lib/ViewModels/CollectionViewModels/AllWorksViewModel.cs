using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllWorksViewModel : ViewModelBase
    {
        private const string _ROUTE = nameof(AllWorksViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Title"] = nameof(Work.Title),
            ["Composer name"] = nameof(Work.ComposerName)
        };

        private readonly WorkService _service;
        private readonly AsyncRelayCommand _addNewCommand;
        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _selectedOrdering;

        [ObservableProperty]
        private ObservableCollection<Work> _collection;

        [ObservableProperty]
        private Work? _selectedWork;

        public AllWorksViewModel()
        {
            _service = new(_database);
            _addNewCommand = new(AddNewAsync);
            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);

            _searchText = string.Empty;
            _selectedOrdering = _orderings.Keys.First();
            _collection = [];
        }

        public static string Route => _ROUTE;

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
            throw new NotImplementedException();
        }

        private async Task SelectAsync()
        {
            throw new NotImplementedException();
        }
    }
}