using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditRepertoireViewModel : EditViewModelBase<Repertoire>, IQueryAttributable
    {
        public const string ROUTE = nameof(EditRepertoireViewModel);
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private const string _NO_WORK_SELECTED_ERROR = "No work selected";
        private const string _EDIT_PAGE_TITLE = "Edit repertoire";
        private const string _NEW_PAGE_TITLE = "New repertoire";

        private readonly RepertoireService _repertoireService;
        private readonly WorkService _workService;

        private readonly AsyncRelayCommand _searchWorksCommand;
        private readonly AsyncRelayCommand _addNewWorkCommand;

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

        [ObservableProperty]
        private string _workTitle = string.Empty;

        [ObservableProperty]
        private string _workSearchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Work> _works = [];

        [ObservableProperty]
        private Work? _selectedWork;

        [ObservableProperty]
        private string _workError = string.Empty;

        public EditRepertoireViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _repertoireService = new(_database);
            _workService = new(_database);
            _searchWorksCommand = new(SearchWorksAsync);
            _addNewWorkCommand = new(AddNewWorkAsync);
        }

        public AsyncRelayCommand SearchWorksCommand => _searchWorksCommand;

        public AsyncRelayCommand AddNewWorkCommand => _addNewWorkCommand;

        protected override IService<Repertoire> Service => _repertoireService;

        private async Task SearchWorksAsync()
        {
            IEnumerable<Work> works = await _workService.SearchAsync(WorkSearchText, nameof(Work.Title));

            Works.Clear();
            foreach (Work work in works)
            {
                Works.Add(work);
            }
        }

        partial void OnSelectedWorkChanged(Work? value)
        {
            if (value is not null)
            {
                WorkTitle = value.Title;
            }
        }

        private async Task AddNewWorkAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [IS_NEW_PARAMETER] = true
            };
            await GoToAsync(parameters, EditWorkViewModel.ROUTE);
        }

        protected override void SetDisplayValues()
        {
            SetDateStarted();
            Syllabus = _value.Syllabus;
            IsFinishedLearning = _value.IsFinishedLearning;
            Notes = _value.Notes;
            WorkTitle = _value.Title;
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

        protected override Task<bool> TrySetValuesToSave()
        {
            SaveDateStarted();
            _value.Syllabus = Syllabus;
            _value.IsFinishedLearning = IsFinishedLearning;
            _value.Notes = Notes;

            bool canSave = true;
            canSave &= TrySetPupilIdToSave();
            canSave &= TrySetWorkToSave();

            return Task.FromResult(canSave);
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

        private bool TrySetWorkToSave()
        {
            if (SelectedWork is Work work)
            {
                _value.WorkId = work.Id;
                _value.Title = work.Title;
                WorkError = string.Empty;
                return true;
            }
            else if (_isNew)
            {
                WorkError = _NO_WORK_SELECTED_ERROR;
                return false;
            }
            else
            {
                WorkError = string.Empty;
                return true;
            }
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                _pupilId = pupilId;
                _repertoireService.PupilId = _pupilId;
            }
            base.ApplyQueryAttributes(query);
        }
    }
}