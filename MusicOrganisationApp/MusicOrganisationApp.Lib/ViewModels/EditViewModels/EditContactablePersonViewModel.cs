using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Tables;
using System.Text.RegularExpressions;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditContactablePersonViewModel<T> : EditViewModelBase<T> where T : class, IContactablePerson, new()
    {
        private const string _EMAIL_REGEX = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
        private const string _PHONE_NUMBER_REGEX = "^\\+?[0-9]+(\\x20[0-9]+)*$";
        private const string _INVALID_EMAIL_ERROR = "Invalid email format";
        private const string _INVALID_PHONE_NUMBER_ERROR = "Invalid phone number format";

        private static Regex _emailRegex = GenerateEmailRegex();
        private static Regex _phoneNumberRegex = GeneratePhoneNumberRegex();

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _emailAddress = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _nameError = string.Empty;

        [ObservableProperty]
        private string _emailAddressError = string.Empty;

        [ObservableProperty]
        private string _phoneNumberError = string.Empty;

        public EditContactablePersonViewModel(string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {
        }

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            EmailAddress = _value.EmailAddress;
            PhoneNumber = _value.PhoneNumber;
            SetNonContactInfoDisplayValues();
        }

        protected abstract void SetNonContactInfoDisplayValues();

        protected override Task<bool> TrySetValuesToSave()
        {
            bool canSave = true;

            canSave &= TrySetNameToSave();
            canSave &= TrySetEmailAddressToSave();
            canSave &= TrySetPhoneNumberToSave();
            canSave &= TrySetNonContactInfoToSave();

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

        private bool TrySetEmailAddressToSave()
        {
            if (EmailAddress == string.Empty || IsValidEmailAddress(EmailAddress))
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
            if (PhoneNumber == string.Empty || IsValidPhoneNumber(PhoneNumber))
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