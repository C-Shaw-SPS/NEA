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
        public const string COMPOSER_ID_PARAMETER = nameof(COMPOSER_ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);

        private const string _EDIT_COMPOSER = "Edit composer";
        private const string _NEW_COMPOSER = "New composer";

        private const string _BLANK_NAME_ERROR = "Name must not be blank";
        private const string _INVALID_YEAR_FORMAT_ERROR = "Invalid year format";
        private const string _NEGATIVE_YEAR_ERROR = "Year cannot be less than zero";
        private const string _DEATH_BEFORE_BIRTH_ERROR = "Year of death cannot be before year of birth";

        private readonly ComposerService _composerService;
        private ComposerData _composer;
        private bool _isNew;
        private int _id;

        [ObservableProperty]
        private string _pageTitle;

        [ObservableProperty]
        private bool _canDelete;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _era;

        [ObservableProperty]
        private string _birthYear;

        [ObservableProperty]
        private string _deathYear;

        [ObservableProperty]
        private string _nameError;

        [ObservableProperty]
        private string _birthYearError;

        [ObservableProperty]
        private string _deathYearError;

        private readonly AsyncRelayCommand _saveCommand;

        private readonly AsyncRelayCommand _deleteCommand;

        public EditComposerViewModel()
        {
            _pageTitle = _EDIT_COMPOSER;
            _canDelete = true;

            _composerService = new(_databasePath);
            _composer = new();
            _isNew = false;

            _name = string.Empty;
            _era = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;

            _nameError = string.Empty;
            _birthYearError = string.Empty;
            _deathYearError = string.Empty;

            _saveCommand = new(TrySaveAsync);
            _deleteCommand = new(DeleteAsync);
        }

        public AsyncRelayCommand SaveCommand => _saveCommand;

        public AsyncRelayCommand DeleteCommand => _deleteCommand;


        #region Saving
        private async Task TrySaveAsync()
        {
            SetComposerId();
            SetComposerEra();

            bool canSave = true;
            canSave &= TrySetComposerName();
            canSave &= TrySetComposerBirthYear();
            canSave &= TrySetComposerDeathYear();

            if (canSave)
            {
                if (_isNew)
                {
                    await SaveNewAsync();
                }
                else
                {
                    await SaveExistingAsync();
                }
            }
        }

        private async Task SaveNewAsync()
        {
            _isNew = false;
            CanDelete = true;
            await _composerService.InsertAsync(_composer);
            Dictionary<string, object> parameters = new()
            {
                [ComposerViewModel.COMPOSER_ID] = _composer.Id
            };
            await GoToAsync(parameters, _RETURN, ComposerViewModel.ROUTE);
        }

        private async Task SaveExistingAsync()
        {
            await _composerService.UpdateAsync(_composer);
            await GoToAsync(_RETURN);
        }

        private void SetComposerId()
        {
            if (_isNew)
            {
                _composer.Id = _id;
            }
        }

        private bool TrySetComposerName()
        {
            NameError = string.Empty;
            if (Name == string.Empty)
            {
                NameError = _BLANK_NAME_ERROR;
                return false;
            }
            else
            {
                _composer.Name = Name;
                return true;
            }
        }

        private void SetComposerEra()
        {
            _composer.Era = Era;
        }

        private bool TrySetComposerBirthYear()
        {
            BirthYearError = string.Empty;
            if (BirthYear == string.Empty)
            {
                _composer.BirthYear = null;
                return true;
            }
            else if (int.TryParse(BirthYear, out int birthYear))
            {
                if (birthYear >= 0)
                {
                    _composer.BirthYear = birthYear;
                    return true;
                }
                else
                {
                    BirthYear = string.Empty;
                    BirthYearError = _NEGATIVE_YEAR_ERROR;
                    return false;
                }
            }
            else
            {
                BirthYear = string.Empty;
                BirthYearError = _INVALID_YEAR_FORMAT_ERROR;
                return false;
            }
        }

        private bool TrySetComposerDeathYear()
        {
            DeathYearError = string.Empty;
            if (DeathYear == string.Empty)
            {
                _composer.DeathYear = null;
                return true;
            }
            else if (int.TryParse(DeathYear, out int deathYear))
            {
                if (deathYear < 0)
                {
                    DeathYearError = _NEGATIVE_YEAR_ERROR;
                    DeathYear = string.Empty;
                    return false;
                }
                else if (_composer.BirthYear == null || deathYear >= _composer.BirthYear)
                {
                    _composer.DeathYear = deathYear;
                    return true;
                }
                else
                {
                    DeathYearError = _DEATH_BEFORE_BIRTH_ERROR;
                    DeathYear = string.Empty;
                    return false;
                }
            }
            else
            {
                DeathYearError = _INVALID_YEAR_FORMAT_ERROR;
                DeathYear = string.Empty;
                return false;
            }
        }

        #endregion

        private async Task DeleteAsync()
        {
            await _composerService.DeleteAsync(_composer);
            await GoToAsync(_RETURN, _RETURN);
        }

        #region Validation

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

        #endregion

        #region Querying

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(COMPOSER_ID_PARAMETER, out object? value) && value is int composerId)
            {
                await SetComposerAsync(composerId);
            }
            if (query.TryGetValue(IS_NEW_PARAMETER, out  value) && value is bool isNew && isNew)
            {
                await SetCreateNewComposerAsync();
            }
        }

        private async Task SetComposerAsync(int composerId)
        {
            _composer = await _composerService.GetAsync<ComposerData>(composerId);
            Name = _composer.Name;
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

        private async Task SetCreateNewComposerAsync()
        {
            _isNew = true;
            CanDelete = false;
            _id = await _composerService.GetNextIdAsync<ComposerData>();
            PageTitle = _NEW_COMPOSER;
        }

        #endregion
    }
}