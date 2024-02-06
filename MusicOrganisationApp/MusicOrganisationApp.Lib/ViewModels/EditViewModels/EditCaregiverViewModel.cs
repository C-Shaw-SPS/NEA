using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditCaregiverViewModel : EditViewModelBase<CaregiverData>, IQueryAttributable
    {
        public const string ROUTE = nameof(EditCaregiverViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit caregiver";
        private const string _NEW_PAGE_TITLE = "New caregiver";

        private readonly CaregiverService _service;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _nameError = string.Empty;

        public EditCaregiverViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        protected override IService<CaregiverData> Service => _service;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Email = _value.Email;
            PhoneNumber = _value.PhoneNumber;
        }

        protected override Task<bool> TrySetValuesToSave()
        {
            _value.Email = Email;
            _value.PhoneNumber = PhoneNumber;

            bool canSave = TrySetNameToSave();
            return Task.FromResult(canSave);            
        }

        private bool TrySetNameToSave()
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
    }
}
