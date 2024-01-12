﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class ComposerViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ROUTE = nameof(ComposerViewModel);

        public const string COMPOSER_ID = nameof(ComposerData);

        private readonly ComposerService _composerService;

        private ComposerData? _composer;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _birthYear;

        [ObservableProperty]
        private string _deathYear;

        [ObservableProperty]
        private string _era;

        private readonly AsyncRelayCommand _editCommand;

        public ComposerViewModel()
        {
            _composerService = new(_databasePath);
            _name = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;
            _era = string.Empty;
            _editCommand = new(EditAsync);
        }

        public AsyncRelayCommand EditCommand => _editCommand;

        private async Task EditAsync()
        {
            if (_composer is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [EditComposerViewModel.COMPOSER_ID_PARAMETER] = _composer.Id,
                    [EditComposerViewModel.IS_NEW_PARAMETER] = false
                };
                await Shell.Current.GoToAsync(EditComposerViewModel.ROUTE, parameters);
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(COMPOSER_ID, out object? value) && value is int composerId)
            {
                await SetComposer(composerId);
            }
        }

        private async Task SetComposer(int composerId)
        {
            _composer = await _composerService.GetAsync<ComposerData>(composerId);
            Name = _composer.Name;
            Era = _composer.Era;

            if (_composer.BirthYear is int birthYear)
            {
                BirthYear = birthYear.ToString();
            }
            else
            {
                BirthYear = string.Empty;
            }

            if (_composer.DeathYear is int deathYear)
            {
                DeathYear = deathYear.ToString();
            }
            else
            {
                DeathYear = string.Empty;
            }
        }
    }
}