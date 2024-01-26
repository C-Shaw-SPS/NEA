using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditWorkViewModel : EditViewModelBase<Work>, IQueryAttributable
    {
        private const string _ROUTE = nameof(EditWorkViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit work";
        private const string _NEW_PAGE_TITLE = "New work";
        private const string _BLANK_TITLE_ERROR = "Title cannot be blank";
        private const string _NO_COMPOSER_SELECTED_ERROR = "Work must have a composer";

        private readonly WorkService _workService;
        private readonly ComposerService _composerService;
        private readonly AsyncRelayCommand _searchComposersCommand;
        private readonly AsyncRelayCommand _addNewComposerCommand;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private string _genre = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private string _composerName = string.Empty;

        [ObservableProperty]
        private string _composerSearchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<ComposerData> _composers = [];

        [ObservableProperty]
        private ComposerData? _selectedComposer;

        [ObservableProperty]
        private string _titleError = string.Empty;

        [ObservableProperty]
        private string _composerError = string.Empty;

        public EditWorkViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _workService = new(_database);
            _composerService = new(_database);
            _searchComposersCommand = new(SearchComposersAsync);
            _addNewComposerCommand = new(AddNewComposerAsync);
        }

        protected override IService<Work> Service => _workService;

        public AsyncRelayCommand SearchComposersCommand => _searchComposersCommand;

        public AsyncRelayCommand AddNewComposerCommand => _addNewComposerCommand;

        public static string Route => _ROUTE;

        private async Task SearchComposersAsync()
        {
            IEnumerable<ComposerData> searchResult = await _composerService.SearchAsync(ComposerSearchText, nameof(ComposerData.Name));
            Composers.Clear();
            foreach (ComposerData composer in searchResult)
            {
                Composers.Add(composer);
            }
        }

        private async Task AddNewComposerAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [IS_NEW_PARAMETER] = true
            };
            await GoToAsync(parameters, EditComposerViewModel.Route);
        }

        protected override void SetDisplayValues()
        {
            Title = _value.Title;
            Subtitle = _value.Subtitle;
            Genre = _value.Genre;
            Notes = _value.Notes;
            ComposerName = _value.ComposerName;
        }

        partial void OnSelectedComposerChanged(ComposerData? value)
        {
            if (value is not null)
            {
                ComposerName = value.Name;
            }
        }

        #region Data Validation

        protected override bool TrySetValuesToSave()
        {
            _value.Subtitle = Subtitle;
            _value.Genre = Genre;
            _value.Notes = Notes;

            bool canSave = true;
            canSave &= TrySetTitle();
            canSave &= TrySetComposer();

            return canSave;
        }

        private bool TrySetTitle()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                TitleError = _BLANK_TITLE_ERROR;
                Title = string.Empty;
                return false;
            }
            else
            {
                TitleError = string.Empty;
                _value.Title = Title;
                return true;
            }
        }

        private bool TrySetComposer()
        {
            if (SelectedComposer is not null)
            {
                _value.ComposerId = SelectedComposer.Id;
                _value.ComposerName = SelectedComposer.Name;
                ComposerError = string.Empty;
                return true;
            }
            else if (_isNew)
            {
                ComposerError = _NO_COMPOSER_SELECTED_ERROR;
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}