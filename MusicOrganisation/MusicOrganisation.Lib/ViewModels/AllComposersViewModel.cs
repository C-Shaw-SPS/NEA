﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System.Collections.ObjectModel;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private const int _LIMIT = 100;

        public const string ROUTE = nameof(AllComposersViewModel);

        private static Dictionary<string, string> _orderings = new()
        {
            { "Name", nameof(ComposerData.Name) },
            { "Complete name", nameof(ComposerData.CompleteName) },
            { "Date of birth", nameof(ComposerData.BirthDate) },
            { "Date of death", nameof(ComposerData.DeathDate) }
        };

        private ComposerService _composerService;
        private string _path;

        private AsyncRelayCommand _refreshCommand;
        private AsyncRelayCommand _initialiseCommand;
        private AsyncRelayCommand _searchCommand;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _ordering;
        
        [ObservableProperty]
        private ObservableCollection<ComposerData> _composers;

        public AllComposersViewModel()
        {
            _path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _composerService = new(_path);
            _composers = [];
            _searchText = string.Empty;
            _ordering = _orderings.Keys.First();
            _refreshCommand = new(RefreshAsync);
            _initialiseCommand = new(InitialiseAsync);
            _searchCommand = new(SearchAsync);
        }

        public IEnumerable<string> Orderings => _orderings.Values;

        public AsyncRelayCommand RefreshCommand => _refreshCommand;

        public AsyncRelayCommand InitialiseCommand => _initialiseCommand;

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public async Task RefreshAsync()
        {
            Composers.Clear();
            IEnumerable<ComposerData> composers = await _composerService.GetAllAsync<ComposerData>();
            foreach (ComposerData composer in composers)
            {
                Composers.Add(composer);
            }
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
            IEnumerable<ComposerData> composers = await _composerService.SearchAsync<ComposerData>(nameof(ComposerData.CompleteName), SearchText, Ordering, _LIMIT);
            Composers.Clear();
            foreach (ComposerData compsoer in composers)
            {
                Composers.Add(compsoer);
            }
        }
    }
}