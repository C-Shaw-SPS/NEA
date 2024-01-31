using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class PupilViewModel : ModelViewModelBase<Pupil>, IQueryAttributable
    {
        private const string _ROUTE = nameof(PupilViewModel);

        private readonly PupilService _service;
        private readonly AsyncRelayCommand _goToRepertoireCommand;

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

        public PupilViewModel() : base(EditPupilViewModel.Route)
        {
            _service = new(_database);
            _goToRepertoireCommand = new(GoToRepertoireAsync);
        }

        public static string Route => _ROUTE;

        protected override IService<Pupil> Service => _service;

        public AsyncRelayCommand GoToRepertoireCommand => _goToRepertoireCommand;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Level = _value.Level;
            NeedsDifferentTimes = _value.NeedsDifferentTimes;
            LessonDuration = _value.LessonDuration.ToString();
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
            await GoToAsync(parameters, AllRepertoireViewModel.Route);
        }
    }
}