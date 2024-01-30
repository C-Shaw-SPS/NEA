using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using SQLitePCL;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditPupilViewModel : EditViewModelBase<Pupil>, IQueryAttributable
    {
        private const string _ROUTE = nameof(EditPupilViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit pupil";
        private const string _NEW_PAGE_TITLE = "New pupil";

        private const string _BLANK_NAME_ERROR = "Name cannot be blank";
        private const string _INVALID_DURATION_ERROR = "Invalid lesson duration";

        private readonly PupilService _service;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _level = string.Empty;

        [ObservableProperty]
        private bool _needsDifferentTimes = false;

        [ObservableProperty]
        private string _lessonDurationHours = string.Empty;

        [ObservableProperty]
        private string _lessonDurationMinutes = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private string _nameError = string.Empty;

        [ObservableProperty]
        private string _lessonDurationError = string.Empty;

        public EditPupilViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Pupil> Service => _service;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Level = _value.Level;
            NeedsDifferentTimes = _value.NeedsDifferentTimes;
            LessonDurationHours = _value.LessonDuration.Hours.ToString();
            LessonDurationMinutes = _value.LessonDuration.Minutes.ToString();
            Email = _value.Email;
            PhoneNumber = _value.PhoneNumber;
            Notes = _value.Notes;
        }

        protected override bool TrySetValuesToSave()
        {
            _value.Level = Level;
            _value.NeedsDifferentTimes = NeedsDifferentTimes;
            _value.Email = Email;
            _value.PhoneNumber = PhoneNumber;
            _value.Notes = Notes;

            bool canSave = true;
            canSave &= TrySetName();
            canSave &= TrySetLessonDuration();

            return canSave;
        }

        private bool TrySetName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = _BLANK_NAME_ERROR;
                return false;
            }
            else
            {
                _value.Name = Name;
                NameError = string.Empty;
                return true;
            }
        }

        private bool TrySetLessonDuration()
        {
            bool validHours = TryGetPositiveInt(LessonDurationHours, out int hours);
            bool validMinutes = TryGetPositiveInt(LessonDurationHours, out int minutes);

            if (validHours && validMinutes)
            {
                TimeSpan lessonDuration = new(hours, minutes, 0);
                _value.LessonDuration = lessonDuration;
                LessonDurationHours = lessonDuration.Hours.ToString();
                LessonDurationMinutes = lessonDuration.Minutes.ToString();
                LessonDurationError = string.Empty;
                return true;
            }
            else
            {
                LessonDurationHours = string.Empty;
                LessonDurationMinutes = string.Empty;
                LessonDurationError = _INVALID_DURATION_ERROR;
                return false;
            }
        }
    }
}