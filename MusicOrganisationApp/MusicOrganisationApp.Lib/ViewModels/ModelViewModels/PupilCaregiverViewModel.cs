using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class PupilCaregiverViewModel : ModelViewModelBase<PupilCaregiver>, IQueryAttributable
    {
        public const string ROUTE = nameof(PupilCaregiverViewModel);

        private readonly PupilCaregiverService _service;
        private readonly AsyncRelayCommand _goToCaregiverCommand;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        public PupilCaregiverViewModel() : base(EditPupilCaregiverViewModel.ROUTE)
        {
            _service = new(_database);
            _goToCaregiverCommand = new(GoToCaregiverAsync);
        }

        protected override IService<PupilCaregiver> Service => _service;

        public AsyncRelayCommand GoToCaregiverCommand => _goToCaregiverCommand;

        private async Task GoToCaregiverAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [ID_PARAMETER] = _value.Id
            };
            await GoToAsync(parameters, CaregiverViewModel.ROUTE);
        }

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Email = _value.Email;
            PhoneNumber = _value.PhoneNumber;
            Description = _value.Description;
        }

        protected override void AddEditRouteParameters(Dictionary<string, object> parameters)
        {
            parameters[EditPupilCaregiverViewModel.PUPIL_ID_PARAMETER] = _value.PupilId;
        }
    }
}