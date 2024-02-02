using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class CaregiverViewModel : ModelViewModelBase<CaregiverData>
    {
        private const string _ROUTE = nameof(CaregiverViewModel);

        private readonly CaregiverService _service;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        public CaregiverViewModel() : base(EditCaregiverViewModel.Route)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<CaregiverData> Service => _service;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Email = _value.Email;
            PhoneNumber = _value.PhoneNumber;
        }
    }
}