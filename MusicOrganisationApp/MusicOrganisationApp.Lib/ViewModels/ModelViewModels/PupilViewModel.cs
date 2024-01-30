using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class PupilViewModel : ModelViewModelBase<Pupil>
    {
        private const string _ROUTE = nameof(PupilViewModel);

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

        public PupilViewModel() : base(EditPupilViewModel.Route)
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
    }
}