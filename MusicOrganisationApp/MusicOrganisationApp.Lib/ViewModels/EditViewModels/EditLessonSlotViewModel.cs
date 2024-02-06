using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditLessonSlotViewModel : EditViewModelBase<LessonSlotData>, IQueryAttributable
    {
        public const string ROUTE = nameof(EditLessonSlotViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit lesson slot";
        private const string _NEW_PAGE_TITLE = "New lesson slot";
        private const string _END_BEFORE_START_ERROR = "End time cannot be before start time";
        private const string _CLASH_ERROR = "Lesson slot clashes";

        private static readonly List<DayOfWeek> _daysOfWeek = LessonSlotService.GetDaysOfWeek();

        private readonly LessonSlotService _service;
        private readonly AsyncRelayCommand _goToClashingLessonSlotCommand;

        [ObservableProperty]
        private DayOfWeek _dayOfWeek = DayOfWeek.Monday;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private string _timeError = string.Empty;

        [ObservableProperty]
        private ObservableCollection<LessonSlotData> _clashingLessonSlots = [];

        [ObservableProperty]
        private LessonSlotData? _selectedClashingLessonSlot;

        public EditLessonSlotViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
            _goToClashingLessonSlotCommand = new(GoToClashingLessonSlot);
        }

        public List<DayOfWeek> DaysOfWeek => _daysOfWeek;

        public AsyncRelayCommand GoToClashingLessonSlotCommand => _goToClashingLessonSlotCommand;

        protected override IService<LessonSlotData> Service => _service;

        private async Task GoToClashingLessonSlot()
        {
            if (SelectedClashingLessonSlot is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedClashingLessonSlot.Id
                };
                await GoToAsync(parameters, LessonSlotViewModel.ROUTE);
            }
        }

        protected override void SetDisplayValues()
        {
            DayOfWeek = _value.DayOfWeek;
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
        }

        protected async override Task<bool> TrySetValuesToSave()
        {
            _value.DayOfWeek = DayOfWeek;
            bool canSave = await TrySetTimesToSave();
            return canSave;
        }

        private async Task<bool> TrySetTimesToSave()
        {
            if (EndTime < StartTime)
            {
                TimeError = _END_BEFORE_START_ERROR;
                return false;
            }
            int? id = _isNew ? null : _value.Id;
            IEnumerable<LessonSlotData> clashingLessonSlots = await _service.GetClashingLessonsAsync(DayOfWeek, StartTime, EndTime, id);
            if (clashingLessonSlots.Any())
            {
                TimeError = _CLASH_ERROR;
                ResetCollection(ClashingLessonSlots, clashingLessonSlots);
                return false;
            }
            else
            {
                TimeError = string.Empty;
                ClashingLessonSlots.Clear();
                _value.StartTime = StartTime;
                _value.EndTime = EndTime;
                return true;
            }
        }

        partial void OnDayOfWeekChanged(DayOfWeek value)
        {
            _service.DayOfWeek = value;
        }
    }
}