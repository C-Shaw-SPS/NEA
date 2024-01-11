using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private const int _GROUP_SIZE = 128;

        public const string ROUTE = nameof(AllComposersViewModel);

        private readonly Dictionary<string, string> _orderings = new()
        {
            { "Name", nameof(ComposerData.Name) },
            { "Complete name", nameof(ComposerData.CompleteName) },
            { "Year of birth", nameof(ComposerData.BirthYear) },
            { "Year of death", nameof(ComposerData.DeathYear) }
        };

        private readonly ComposerService _composerService;
        private int _groupCount;

        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _loadMoreCommand;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _ordering;
        
        [ObservableProperty]
        private List<ComposerData> _composers;

        [ObservableProperty]
        private ComposerData? _selectedComposer;

        public AllComposersViewModel()
        {
            _composerService = new(_databasePath);
            _groupCount = 1;
            _composers = [];
            _searchText = string.Empty;
            _ordering = _orderings.Keys.First();
            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);
            _loadMoreCommand = new(LoadMoreAsync);
        }

        public List<string> Orderings => new(_orderings.Keys);

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand LoadMoreCommand => _loadMoreCommand;

        public async Task RefreshAsync()
        {
            _groupCount = 1;
            await SearchAsync();
        }

        private async Task InitialiseAsync()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            await _composerService.ClearTableAsync<ComposerData>();
            await _composerService.InsertAllAsync(composers);
            await RefreshAsync();
        }

        private async Task SearchAsync()
        {
            _groupCount = 1;
            await LoadAsync();
        }

        private async Task LoadMoreAsync()
        {
            int prevLimit = _groupCount * _GROUP_SIZE;
            if (Composers.Count == prevLimit)
            {
                ++_groupCount;
                await LoadAsync();
            }
        }

        private async Task LoadAsync()
        {
            string ordering = _orderings[Ordering];
            int limit = _groupCount * _GROUP_SIZE;
            IEnumerable<ComposerData> composers = await _composerService.SearchAsync<ComposerData>(nameof(ComposerData.CompleteName), SearchText, ordering, limit);
            Composers.Clear();
            Composers.AddRange(composers);
        }

        private async Task SelectAsync()
        {
            if (SelectedComposer is not null)
            {
                Dictionary<string, object> routeParameters = new()
                {
                    [ComposerViewModel.QUERY_PARAMETER] = SelectedComposer
                };
                await Shell.Current.GoToAsync(ComposerViewModel.ROUTE, routeParameters);
            }
        }

        async partial void OnOrderingChanged(string value)
        {
            await SearchAsync();
        }
    }
}