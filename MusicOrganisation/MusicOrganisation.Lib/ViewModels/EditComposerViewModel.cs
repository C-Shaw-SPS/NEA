using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class EditComposerViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ROUTE = nameof(EditComposerViewModel);
        public const string COMPOSER_PARAMETER = nameof(COMPOSER_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);

        private readonly ComposerService _composerService;
        private ComposerData _composer;
        private bool _isNew;
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _completeName;

        [ObservableProperty]
        private string _era;

        [ObservableProperty]
        private string _birthYear;

        [ObservableProperty]
        private string _deathYear;

        private readonly AsyncRelayCommand _saveCommand;

        public EditComposerViewModel()
        {
            _composerService = new(_databasePath);
            _composer = new();
            _isNew = false;

            _name = string.Empty;
            _completeName = string.Empty;
            _era = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;
            _saveCommand = new(SaveAsync);
        }

        public AsyncRelayCommand SaveCommand => _saveCommand;

        private async Task SaveAsync()
        {
            if (_isNew)
            {
                _composer.Id = _id;
            }
            _composer.Name = Name;
            _composer.CompleteName = CompleteName;
            _composer.Era = Era;

            if (BirthYear == string.Empty)
            {
                _composer.BirthYear = null;
            }
            else if (int.TryParse(BirthYear, out int birthYear) && birthYear >= 0)
            {
                _composer.BirthYear = birthYear;
            }
            else
            {
                BirthYear = _composer.BirthYear.ToString()?? string.Empty;
            }

            if (int.TryParse(DeathYear, out int deathYear) && deathYear >= (_composer.BirthYear?? 0))
            {
                _composer.DeathYear = deathYear;
            }
            else
            {
                _composer.DeathYear = null;
                DeathYear = string.Empty;
            }

            if (_isNew)
            {
                _isNew = false;
                await _composerService.InsertAsync(_composer);
            }
            else
            {
                await _composerService.UpdateAsync(_composer);
            }
        }

        partial void OnBirthYearChanged(string? oldValue, string newValue)
        {
            if (newValue == string.Empty)
            {
                return;
            }
            if (int.TryParse(newValue, out int birthYear) && birthYear >= 0)
            {
                return;
            }
            BirthYear = oldValue?? string.Empty;
        }

        partial void OnDeathYearChanged(string? oldValue, string newValue)
        {
            if (newValue == string.Empty)
            {
                return;
            }
            if (int.TryParse(newValue, out int deathYear) && deathYear >= 0)
            {
                return;
            }
            DeathYear = oldValue ?? string.Empty;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(COMPOSER_PARAMETER, out object? value) && value is ComposerData composer)
            {
                SetComposer(composer);
            }
            if (query.TryGetValue(IS_NEW_PARAMETER, out  value) && value is bool isNew && isNew)
            {
                await SetNewComposer();
            }
        }

        private void SetComposer(ComposerData composer)
        {
            _composer = composer;
            Name = _composer.Name;
            CompleteName = _composer.CompleteName;
            Era = _composer.Era;
            if (_composer.BirthYear is int birthYear)
            {
                BirthYear = birthYear.ToString();
            }
            if (_composer.DeathYear is int deathYear)
            {
                DeathYear = deathYear.ToString();
            }
            _id = _composer.Id;
        }

        private async Task SetNewComposer()
        {
            _isNew = true;
            _id = await _composerService.GetNextIdAsync<ComposerData>();
        }
    }
}