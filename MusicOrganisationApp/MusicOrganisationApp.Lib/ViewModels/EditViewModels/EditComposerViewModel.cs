using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditComposerViewModel : ViewModelBase
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

        private async Task TrySaveAsync()
        {
            throw new NotImplementedException();
        }

        private async Task DeleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}