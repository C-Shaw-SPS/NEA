using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);
    }

    public abstract partial class EditViewModelBase<T> : EditViewModelBase, IQueryAttributable where T : class, IIdentifiable, new()
    {
        private readonly string _editPageTitle;
        private readonly string _newPageTitle;

        private int _id;
        private bool _isNew;
        private T _value;

        private readonly AsyncRelayCommand _trySaveCommand;
        private readonly AsyncRelayCommand _deleteCommand;

        [ObservableProperty]
        private string _pageTitle;

        [ObservableProperty]
        private bool _canDelete;

        public EditViewModelBase(string editPageTitle, string newPageTitle)
        {
            _editPageTitle = editPageTitle;
            _newPageTitle = newPageTitle;

            _isNew = false;
            _value = new();

            _pageTitle = _editPageTitle;
            _canDelete = true;

            _trySaveCommand = new(TrySaveAsync);
            _deleteCommand = new(DeleteAsync);
        }

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

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
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
                _id = id;
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
    }
}