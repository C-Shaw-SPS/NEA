using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class PupilViewModel : ModelViewModelBase<Pupil>, IQueryAttributable
    {
        public const string ROUTE = nameof(PupilViewModel);

        private const string _TIMESPAN_FORMAT = "hh\\:mm";

        private readonly PupilService _service;
        private readonly AsyncRelayCommand _goToRepertoireCommand;
        private readonly AsyncRelayCommand _goToCaregiversCommand;
        private readonly AsyncRelayCommand _goToLessonsCommand;
        private readonly AsyncRelayCommand _goToAvaliabilityCommand;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _level = string.Empty;

        [ObservableProperty]
        private bool _needsDifferentTimes = false;

        [ObservableProperty]
        private string _lessonDuration = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        public PupilViewModel() : base(EditPupilViewModel.ROUTE)
        {
            _service = new(_database);
            _goToRepertoireCommand = new(GoToRepertoireAsync);
            _goToCaregiversCommand = new(GoToCaregiversAsync);
            _goToLessonsCommand = new(GoToLessonsAsync);
            _goToAvaliabilityCommand = new(GoToAvaliabilityAsnyc);
        }

        protected override IService<Pupil> Service => _service;

        public AsyncRelayCommand GoToRepertoireCommand => _goToRepertoireCommand;

        public AsyncRelayCommand GoToCaregiversCommand => _goToCaregiversCommand;

        public AsyncRelayCommand GoToLessonsCommand => _goToLessonsCommand;

        public AsyncRelayCommand GoToAvaliabilityCommand => _goToAvaliabilityCommand;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Level = _value.Level;
            NeedsDifferentTimes = _value.NeedsDifferentTimes;
            LessonDuration = _value.LessonDuration.ToString(_TIMESPAN_FORMAT);
            Email = _value.Email;
            PhoneNumber = _value.PhoneNumber;
            Notes = _value.Notes;
        }

        private async Task GoToRepertoireAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [AllRepertoireViewModel.PUPIL_ID_PARAMETER] = _value.Id
            };
            await GoToAsync(parameters, AllRepertoireViewModel.ROUTE);
        }

        private async Task GoToCaregiversAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [AllPupilCaregiversViewModel.PUPIL_ID_PARAMETER] = _value.Id
            };
            await GoToAsync(parameters, AllPupilCaregiversViewModel.ROUTE);
        }

        private async Task GoToLessonsAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [AllPupilLessonsViewModel.PUPIL_ID_PARAMETER] = _value.Id
            };
            await GoToAsync(parameters, AllPupilLessonsViewModel.ROUTE);
        }

        private async Task GoToAvaliabilityAsnyc()
        {
            Dictionary<string, object> parameters = new()
            {
                [AllPupilLessonsViewModel.PUPIL_ID_PARAMETER] = _value.Id
            };
            await GoToAsync(parameters, PupilAvaliabilityViewModel.ROUTE);
        }
    }
}