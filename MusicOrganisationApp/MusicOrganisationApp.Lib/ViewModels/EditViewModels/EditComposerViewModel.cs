using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditComposerViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);

        private const string _ROUTE = nameof(EditComposerViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit composer";
        private const string _NEW_PAGE_TITLE = "New composer";

        private const string _BLANK_NAME_ERROR = "Name cannot be blank";
        private const string _INVALID_YEAR_FORMAT_ERROR = "Invalid year format";
        private const string _NEGATIVE_YEAR_ERROR = "Year cannot be negative";
        private const string _DEATH_BEFORE_BIRTH_ERROR = "Year of death cannot be before year of birth";

        private int _id;
        private bool _isNew;
        private ComposerData _value;
        private readonly ComposerService _service;

        private readonly AsyncRelayCommand _trySaveCommand;
        private readonly AsyncRelayCommand _deleteCommand;

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

        public EditComposerViewModel()
        {
            _isNew = false;
            _value = new();
            _service = new(_database);

            _pageTitle = _EDIT_PAGE_TITLE;
            _canDelete = true;

            _trySaveCommand = new(TrySaveAsync);
            _deleteCommand = new(DeleteAsync);

            _name = string.Empty;
            _era = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;

            _nameError = string.Empty;
            _birthYearError = string.Empty;
            _deathYearError = string.Empty;
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand TrySaveCommand => _trySaveCommand;

        public AsyncRelayCommand DeleteCommand => _deleteCommand;

        #region Saving

        private async Task TrySaveAsync()
        {
            bool canSave = TrySetValuesToSave();
            if (canSave)
            {
                if (_isNew)
                {
                    await _service.InsertAsync(_value, true);
                }
                else
                {
                    await _service.UpdateAsync(_value);
                }
                await GoBackAsync();
            }
        }

        private bool TrySetValuesToSave()
        {
            SetComposerEra();

            bool canSave = true;
            canSave &= TrySetComposerName();
            canSave &= TrySetComposerBirthYear();
            canSave &= TrySetComposerDeathYear();

            return canSave;
        }

        private void SetComposerEra()
        {
            _value.Era = Era;
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
                _value.Name = Name;
                return true;
            }
        }

        private bool TrySetComposerBirthYear()
        {
            BirthYearError = string.Empty;
            if (BirthYear == string.Empty)
            {
                _value.BirthYear = null;
                return true;
            }
            else if (int.TryParse(BirthYear, out int birthYear))
            {
                if (birthYear >= 0)
                {
                    _value.BirthYear = birthYear;
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
                _value.DeathYear = null;
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
                else if (_value.BirthYear == null || deathYear >= _value.BirthYear)
                {
                    _value.DeathYear = deathYear;
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
            await _service.DeleteAsync(_value);
            await GoBackAsync();
        }

        #region Data Validation

        partial void OnBirthYearChanged(string? oldValue, string newValue)
        {
            if (IsNumeric(newValue))
            {
                return;
            }
            BirthYear = oldValue ?? string.Empty;
        }

        partial void OnDeathYearChanged(string? oldValue, string newValue)
        {
            if (IsNumeric(newValue))
            {
                return;
            }
            DeathYear = oldValue ?? string.Empty;
        }

        private static bool IsNumeric(string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Page Setup

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetComposer(id);
            }
            if (query.TryGetValue(IS_NEW_PARAMETER, out value) && value is bool isNew && isNew)
            {
                SetCreateNew();
            }
        }

        private async Task SetComposer(int id)
        {
            (bool suceeded, ComposerData composer) = await _service.GetAsync(id);
            if (suceeded)
            {
                _id = id;
                _value = composer;
                SetDisplayValues();
            }
            else
            {
                await GoBackAsync();
                return;
            }
        }

        private void SetDisplayValues()
        {
            Name = _value.Name;
            Era = _value.Era;
            if (_value.BirthYear is int birthYear)
            {
                BirthYear = birthYear.ToString();
            }
            if (_value.DeathYear is int deathYear)
            {
                DeathYear = deathYear.ToString();
            }
        }

        private void SetCreateNew()
        {
            _isNew = true;
            CanDelete = false;
            PageTitle = _NEW_PAGE_TITLE;
        }

        #endregion
    }
}