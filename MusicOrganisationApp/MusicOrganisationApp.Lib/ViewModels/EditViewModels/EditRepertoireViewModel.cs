using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditRepertoireViewModel : SearchableEditViewModel<Repertoire, Work, EditWorkViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditRepertoireViewModel);
        private const string _NO_WORK_SELECTED_ERROR = "No work selected";
        private const string _EDIT_PAGE_TITLE = "Edit repertoire";
        private const string _NEW_PAGE_TITLE = "New repertoire";
        private const string _SEARCH_ORDERING = nameof(WorkData.Title);

        private readonly RepertoireService _repertoireService;
        private readonly WorkService _workService;

        private int? _pupilId;

        [ObservableProperty]
        private DateTime _dateStarted = DateTime.Today;

        [ObservableProperty]
        private bool _hasDateStarted = false;

        [ObservableProperty]
        private string _syllabus = string.Empty;

        [ObservableProperty]
        private bool _isFinishedLearning = false;

        [ObservableProperty]
        private string _notes = string.Empty;

        public EditRepertoireViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE, _NO_WORK_SELECTED_ERROR, GetDefaultPath(), false)
        {
            _repertoireService = new(_database);
            _workService = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Repertoire> Service => _repertoireService;

        protected override ISearchService<Work> SearchService => _workService;

        protected override string SearchOrdering => _SEARCH_ORDERING;

        protected override void SetDisplayValues()
        {
            SetDateStarted();
            Syllabus = _value.Syllabus;
            IsFinishedLearning = _value.IsFinishedLearning;
            Notes = _value.Notes;
            SelectedItemText = _value.Title;
        }

        private void SetDateStarted()
        {
            if (_value.DateStarted is DateTime dateStarted)
            {
                DateStarted = dateStarted;
                HasDateStarted = true;
            }
            else
            {
                HasDateStarted = false;
            }
        }

        public override async Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
        {
            if (query.TryGetValue(IPupilDataViewModel.PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                _pupilId = pupilId;
                _repertoireService.PupilId = _pupilId;
            }
            await base.ApplyQueryAttributesAsync(query);
        }

        protected override bool TrySetNonSearchValuesToSave()
        {
            SaveDateStarted();
            _value.Syllabus = Syllabus;
            _value.IsFinishedLearning = IsFinishedLearning;
            _value.Notes = Notes;
            bool canSave = TrySetPupilIdToSave();
            return canSave;
        }

        private void SaveDateStarted()
        {
            if (HasDateStarted)
            {
                _value.DateStarted = DateStarted;
            }
            else
            {
                _value.DateStarted = null;
            }
        }

        private bool TrySetPupilIdToSave()
        {
            if (_pupilId is int pupilId)
            {
                _value.PupilId = pupilId;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void UpdateSelectedItemText(Work value)
        {
            SelectedItemText = value.Title;
        }

        protected override void SetSearchValuesToSave(Work selectedItem)
        {
            _value.WorkId = selectedItem.Id;
        }
    }
}