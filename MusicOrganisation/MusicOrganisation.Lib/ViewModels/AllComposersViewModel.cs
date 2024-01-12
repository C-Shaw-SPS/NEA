﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System.Collections.ObjectModel;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private const int _LIMIT = 128;

        public const string ROUTE = nameof(AllComposersViewModel);

        private readonly Dictionary<string, string> _orderings = new()
        {
            { "Name", nameof(ComposerData.Name) },
            { "Complete name", nameof(ComposerData.CompleteName) },
            { "Year of birth", nameof(ComposerData.BirthYear) },
            { "Year of death", nameof(ComposerData.DeathYear) }
        };

        private readonly ComposerService _composerService;

        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _newCommand;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _ordering;
        
        [ObservableProperty]
        private ObservableCollection<ComposerData> _composers;

        [ObservableProperty]
        private ComposerData? _selectedComposer;

        public AllComposersViewModel()
        {
            _composerService = new(_databasePath);
            _composers = [];
            _searchText = string.Empty;
            _ordering = _orderings.Keys.First();
            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);
            _newCommand = new(NewAsync);
        }

        public List<string> Orderings => new(_orderings.Keys);

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand NewCommand => _newCommand;

        public async Task RefreshAsync()
        {
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
            string ordering = _orderings[Ordering];
            IEnumerable<ComposerData> composers = await _composerService.SearchAsync<ComposerData>(nameof(ComposerData.CompleteName), SearchText, ordering, _LIMIT);
            Composers.Clear();
            foreach (ComposerData compsoer in composers)
            {
                Composers.Add(compsoer);
            }
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

        private async Task NewAsync()
        {
            Dictionary<string, object> routeParameters = new()
            {
                [EditComposerViewModel.IS_NEW_PARAMETER] = true
            };
            await Shell.Current.GoToAsync(EditComposerViewModel.ROUTE, routeParameters);
        }

        async partial void OnOrderingChanged(string value)
        {
            await SearchAsync();
        }
    }
}