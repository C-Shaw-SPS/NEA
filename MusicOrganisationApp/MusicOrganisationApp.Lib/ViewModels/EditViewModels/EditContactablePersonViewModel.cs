using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Tables;
using System.Text.RegularExpressions;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditContactablePersonViewModel<T> : EditPersonViewModel<T> where T : class, IContactablePerson, new()
    {
        private const string _EMAIL_REGEX = "^([^@\\s]+@[^@\\s]+\\.[^@\\s]+)?$";
        private const string _PHONE_NUMBER_REGEX = "^(\\+?[0-9]+(\\x20[0-9]+)*)?$";
        private const string _INVALID_EMAIL_ERROR = "Invalid email format";
        private const string _INVALID_PHONE_NUMBER_ERROR = "Invalid phone number format";

        private static readonly Regex _emailRegex = GenerateEmailRegex();
        private static readonly Regex _phoneNumberRegex = GeneratePhoneNumberRegex();

        [ObservableProperty]
        private string _emailAddress = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _emailAddressError = string.Empty;

        [ObservableProperty]
        private string _phoneNumberError = string.Empty;

        public EditContactablePersonViewModel(string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {

        }

        public EditContactablePersonViewModel(string editPageTitle, string newPageTitle, string path, bool isTesting) : base(editPageTitle, newPageTitle, path, isTesting)
        {

        }

        protected override void SetNonNameDisplayValues()
        {
            EmailAddress = _value.EmailAddress;
            PhoneNumber = _value.PhoneNumber;
            SetNonContactInfoDisplayValues();
        }

        protected abstract void SetNonContactInfoDisplayValues();

        protected override bool TrySetNonNameValuesToSave()
        {
            bool canSave = true;
            canSave &= TrySetEmailAddressToSave();
            canSave &= TrySetPhoneNumberToSave();
            canSave &= TrySetNonContactInfoToSave();
            return canSave;
        }

        private bool TrySetEmailAddressToSave()
        {
            if (IsValidEmailAddress(EmailAddress))
            {
                _value.EmailAddress = EmailAddress;
                EmailAddressError = string.Empty;
                return true;
            }
            else
            {
                EmailAddressError = _INVALID_EMAIL_ERROR;
                return false;
            }
        }

        private static bool IsValidEmailAddress(string value)
        {
            return _emailRegex.IsMatch(value);
        }

        private bool TrySetPhoneNumberToSave()
        {
            if (IsValidPhoneNumber(PhoneNumber))
            {
                _value.PhoneNumber = PhoneNumber;
                PhoneNumberError = string.Empty;
                return true;
            }
            else
            {
                PhoneNumberError = _INVALID_PHONE_NUMBER_ERROR;
                return false;
            }
        }

        private static bool IsValidPhoneNumber(string value)
        {
            return _phoneNumberRegex.IsMatch(value);
        }

        protected abstract bool TrySetNonContactInfoToSave();

        [GeneratedRegex(_EMAIL_REGEX)]
        private static partial Regex GenerateEmailRegex();

        [GeneratedRegex(_PHONE_NUMBER_REGEX)]
        private static partial Regex GeneratePhoneNumberRegex();
    }
}