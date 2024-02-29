using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditComposerViewModel : EditPersonViewModel<Composer>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditComposerViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit composer";
        private const string _NEW_PAGE_TITLE = "New composer";
        private const string _INVALID_YEAR_FORMAT_ERROR = "Invalid year format";
        private const string _NEGATIVE_YEAR_ERROR = "Year cannot be negative";
        private const string _DEATH_BEFORE_BIRTH_ERROR = "Year of death cannot be before year of birth";

        private readonly ComposerService _service;

        [ObservableProperty]
        private string _era = string.Empty;

        [ObservableProperty]
        private string _birthYear = string.Empty;

        [ObservableProperty]
        private string _deathYear = string.Empty;

        [ObservableProperty]
        private string _birthYearError = string.Empty;

        [ObservableProperty]
        private string _deathYearError = string.Empty;

        public EditComposerViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        public EditComposerViewModel(string path, bool isTesting) : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE, path, isTesting)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Composer> Service => _service;


        #region Saving

        protected override bool TrySetNonNameValuesToSave()
        {
            _value.Era = Era;

            bool canSave = true;
            canSave &= TrySetComposerBirthYear();
            canSave &= TrySetComposerDeathYear();

            return canSave;
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

        #endregion

        #region Page Setup

        protected override void SetNonNameDisplayValues()
        {
            Era = _value.Era;
            BirthYear = _value.BirthYear.ToString() ?? string.Empty;
            DeathYear = _value.DeathYear.ToString() ?? string.Empty;
        }

        #endregion
    }
}