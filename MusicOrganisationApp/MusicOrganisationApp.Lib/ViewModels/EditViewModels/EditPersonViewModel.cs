using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditPersonViewModel<T> : EditViewModelBase<T> where T : class, IPerson, new()
    {
        private const string _BLANK_NAME_ERROR = "Name cannot be blank";

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _nameError = string.Empty;

        public EditPersonViewModel(string editPageTitle, string newPageTitle, string path, bool isTesting) : base(editPageTitle, newPageTitle, path, isTesting) { }

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            SetNonNameDisplayValues();
        }

        protected abstract void SetNonNameDisplayValues();

        protected override Task<bool> TrySetValuesToSaveAsync()
        {
            bool canSave = true;
            canSave &= TrySetNameToSave();
            canSave &= TrySetNonNameValuesToSave();
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

        protected abstract bool TrySetNonNameValuesToSave();
    }
}