using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using SQLitePCL;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditPupilViewModel : EditViewModelBase<Pupil>, IQueryAttributable
    {
        public const string ROUTE = nameof(EditPupilViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit pupil";
        private const string _NEW_PAGE_TITLE = "New pupil";
        private const string _INVALID_DURATION_ERROR = "Invalid lesson duration";

        private readonly PupilService _service;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _level = string.Empty;

        [ObservableProperty]
        private bool _needsDifferentTimes = false;

        [ObservableProperty]
        private string _lessonHours = string.Empty;

        [ObservableProperty]
        private string _lessonMinutes = string.Empty;

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

        protected override IService<Pupil> Service => _service;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Level = _value.Level;
            NeedsDifferentTimes = _value.NeedsDifferentTimes;
            LessonHours = _value.LessonDuration.Hours.ToString();
            LessonMinutes = _value.LessonDuration.Minutes.ToString();
            Email = _value.Email;
            PhoneNumber = _value.PhoneNumber;
            Notes = _value.Notes;
        }

        protected override Task<bool> TrySetValuesToSave()
        {
            _value.Level = Level;
            _value.NeedsDifferentTimes = NeedsDifferentTimes;
            _value.Email = Email;
            _value.PhoneNumber = PhoneNumber;
            _value.Notes = Notes;

            bool canSave = true;
            canSave &= TrySetName();
            canSave &= TrySetLessonDuration();

            return Task.FromResult(canSave);
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
                LessonHours = oldValue?? string.Empty;
            }
        }

        partial void OnLessonMinutesChanged(string? oldValue, string newValue)
        {
            if (!IsNumeric(newValue))
            {
                LessonMinutes = oldValue?? string.Empty;
            }
        }
    }
}