using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditWorkViewModel : SearchableEditViewModel<Work, ComposerData, EditComposerViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditWorkViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit work";
        private const string _NEW_PAGE_TITLE = "New work";
        private const string _BLANK_TITLE_ERROR = "Title cannot be blank";
        private const string _NO_COMPOSER_SELECTED_ERROR = "Work must have a composer";
        private const string _SEARCH_ORDERING = nameof(ComposerData.Name);

        private readonly WorkService _workService;
        private readonly ComposerService _composerService;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private string _genre = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private string _titleError = string.Empty;

        public EditWorkViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE, _NO_COMPOSER_SELECTED_ERROR)
        {
            _workService = new(_database);
            _composerService = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Work> Service => _workService;

        protected override ISearchService<ComposerData> SearchService => _composerService;

        protected override string SearchOrdering => _SEARCH_ORDERING;

        protected override void SetDisplayValues()
        {
            Title = _value.Title;
            Subtitle = _value.Subtitle;
            Genre = _value.Genre;
            Notes = _value.Notes;
            SelectedItemText = _value.ComposerName;
        }

        #region Data Validation

        protected override void UpdateSelectedItemText(ComposerData value)
        {
            SelectedItemText = value.Name;
        }

        protected override void SetSearchValuesToSave(ComposerData selectedItem)
        {
            _value.ComposerId = selectedItem.Id;
        }

        protected override bool TrySetNonSearchValuesToSave()
        {
            _value.Title = Title;
            _value.Subtitle = Subtitle;
            _value.Genre = Genre;
            _value.Notes = Notes;
            bool canSave = TrySetTitleToSave();
            return canSave;
        }

        private bool TrySetTitleToSave()
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

        #endregion
    }
}