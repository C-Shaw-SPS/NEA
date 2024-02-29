using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditPupilViewModel : EditContactablePersonViewModel<Pupil>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditPupilViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit pupil";
        private const string _NEW_PAGE_TITLE = "New pupil";
        private const string _INVALID_DURATION_ERROR = "Invalid lesson duration";

        private readonly PupilService _service;

        [ObservableProperty]
        private string _level = string.Empty;

        [ObservableProperty]
        private bool _needsDifferentTimes = false;

        [ObservableProperty]
        private string _lessonHours = string.Empty;

        [ObservableProperty]
        private string _lessonMinutes = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private string _lessonDurationError = string.Empty;

        public EditPupilViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        public EditPupilViewModel(string path, bool isTesting) : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE, path, isTesting)
        {
            _service = new(_database);    
        }

        public static string Route => _ROUTE;

        protected override IService<Pupil> Service => _service;

        protected override void SetNonContactInfoDisplayValues()
        {
            Level = _value.Level;
            NeedsDifferentTimes = _value.NeedsDifferentTimes;
            LessonHours = _value.LessonDuration.Hours.ToString();
            LessonMinutes = _value.LessonDuration.Minutes.ToString();
            Notes = _value.Notes;
        }

        protected override bool TrySetNonContactInfoToSave()
        {
            _value.Level = Level;
            _value.NeedsDifferentTimes = NeedsDifferentTimes;
            _value.Notes = Notes;
            bool canSave = TrySetLessonDurationToSave();
            return canSave;
        }

        private bool TrySetLessonDurationToSave()
        {
            bool validHours = TryGetPositiveInt(LessonHours, out int hours);
            bool validMinutes = TryGetPositiveInt(LessonMinutes, out int minutes);

            if (validHours && validMinutes)
            {
                TimeSpan lessonDuration = new(hours, minutes, 0);
                _value.LessonDuration = lessonDuration;
                LessonHours = lessonDuration.Hours.ToString();
                LessonMinutes = lessonDuration.Minutes.ToString();
                LessonDurationError = string.Empty;
                return true;
            }
            else
            {
                LessonHours = string.Empty;
                LessonMinutes = string.Empty;
                LessonDurationError = _INVALID_DURATION_ERROR;
                return false;
            }
        }

        partial void OnLessonHoursChanged(string? oldValue, string newValue)
        {
            if (!IsNumeric(newValue))
            {
                LessonHours = oldValue ?? string.Empty;
            }
        }

        partial void OnLessonMinutesChanged(string? oldValue, string newValue)
        {
            if (!IsNumeric(newValue))
            {
                LessonMinutes = oldValue ?? string.Empty;
            }
        }
    }
}