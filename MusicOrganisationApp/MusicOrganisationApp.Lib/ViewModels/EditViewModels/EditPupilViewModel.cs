using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditPupilViewModel : EditViewModelBase<Pupil>, IQueryAttributable
    {
        private const string _ROUTE = nameof(EditPupilViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit pupil";
        private const string _NEW_PAGE_TITLE = "New pupil";

        private const string _BLANK_NAME_ERROR = "Name cannot be blank";
        private const string _NEGATIVE_LESSON_DURATION_ERROR = "Lesson duration cannot be negative";

        private readonly PupilService _service;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _level = string.Empty;

        [ObservableProperty]
        private bool _needsDifferentTimes = false;

        [ObservableProperty]
        private TimeSpan _lessonDuration;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        public string _nameError = string.Empty;

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
            LessonDuration = _value.LessonDuration;
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
            if (LessonDuration < TimeSpan.Zero)
            {
                LessonDurationError = _NEGATIVE_LESSON_DURATION_ERROR;
                return false;
            }
            else
            {
                _value.LessonDuration = LessonDuration;
                LessonDurationError = string.Empty;
                return true;
            }
        }
    }
}