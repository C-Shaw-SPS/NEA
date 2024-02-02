using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Layouts;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);

        protected static bool IsNumeric(string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }

    public abstract partial class EditViewModelBase<T> : EditViewModelBase, IQueryAttributable where T : class, IIdentifiable, new()
    {
        private readonly string _editPageTitle;
        private readonly string _newPageTitle;

        protected bool _isNew = false;
        protected T _value = new();

        private readonly AsyncRelayCommand _trySaveCommand;
        private readonly AsyncRelayCommand _deleteCommand;

        [ObservableProperty]
        private string _pageTitle = string.Empty;

        [ObservableProperty]
        private bool _canDelete = true;

        public EditViewModelBase(string editPageTitle, string newPageTitle)
        {
            _editPageTitle = editPageTitle;
            _newPageTitle = newPageTitle;

            _trySaveCommand = new(TrySaveAsync);
            _deleteCommand = new(DeleteAsync);
        }

        public AsyncRelayCommand TrySaveCommand => _trySaveCommand;

        public AsyncRelayCommand DeleteCommand => _deleteCommand;

        protected abstract IService<T> Service { get; }

        private async Task TrySaveAsync()
        {
            bool canSave = TrySetValuesToSave();
            if (canSave)
            {
                if (_isNew)
                {
                    await Service.InsertAsync(_value, true);
                }
                else
                {
                    await Service.UpdateAsync(_value);
                }
                await GoBackAsync();
            }
        }

        protected abstract bool TrySetValuesToSave();

        private async Task DeleteAsync()
        {
            await Service.DeleteAsync(_value);
            await GoBackAsync();
        }

        public async virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetValue(id);
            }
            if (query.TryGetValue(IS_NEW_PARAMETER, out value) && value is bool isNew)
            {
                SetIsNew(isNew);
            }
        }

        private async Task SetValue(int id)
        {
            (bool suceeded, T value) = await Service.TryGetAsync(id);
            if (suceeded)
            {
                _value = value;
                SetDisplayValues();
            }
            else
            {
                await GoBackAsync();
            }
        }

        protected abstract void SetDisplayValues();

        private void SetIsNew(bool isNew)
        {
            if (isNew)
            {
                _isNew = true;
                PageTitle = _newPageTitle;
                CanDelete = false;
            }
            else
            {
                _isNew = false;
                PageTitle = _editPageTitle;
                CanDelete = true;
            }
        }

        protected static bool TryGetPositiveInt(string input, out int result)
        {
            if (int.TryParse(input, out result) && result >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}