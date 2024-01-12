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
        public const string QUERY_PARAMETER = nameof(ComposerData);

        private readonly ComposerService _composerService;
        private ComposerData _composer;

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
            _composer.Name = Name;
            _composer.CompleteName = CompleteName;
            _composer.Era = Era;

            if (BirthYear == string.Empty)
            {
                _composer.BirthYear = null;
            }
            else if (int.TryParse(BirthYear, out int birthYear))
            {
                _composer.BirthYear = birthYear;
            }
            else
            {
                BirthYear = _composer.BirthYear.ToString()?? string.Empty;
            }

            if (int.TryParse(DeathYear, out int deathYear) && deathYear >= _composer.BirthYear)
            {
                _composer.DeathYear = deathYear;
            }
            else
            {
                _composer.DeathYear = null;
                DeathYear = string.Empty;
            }

            await _composerService.UpdateAsync(_composer);
        }

        partial void OnBirthYearChanged(string? oldValue, string newValue)
        {
            if (newValue != string.Empty && !int.TryParse(newValue, out _))
            {
                BirthYear = oldValue ?? string.Empty;
            }
        }

        partial void OnDeathYearChanged(string? oldValue, string newValue)
        {
            if (newValue != string.Empty && !int.TryParse(newValue, out _))
            {
                DeathYear = oldValue ?? string.Empty;
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(QUERY_PARAMETER, out object? value) && value is ComposerData composer)
            {
                SetComposer(composer);
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
        }
    }
}