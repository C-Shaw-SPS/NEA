using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);

        public EditViewModelBase(string path, bool isTesting) : base(path, isTesting) { }

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

        public EditViewModelBase(string editPageTitle, string newPageTitle, string path, bool isTesting) : base(path, isTesting)
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
            bool canSave = await TrySetValuesToSaveAsync();
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

        protected abstract Task<bool> TrySetValuesToSaveAsync();

        private async Task DeleteAsync()
        {
            await Service.DeleteAsync(_value);
            await GoBackAsync();
        }

        public override async Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetValue(id);
            }
            if (query.TryGetValue(IS_NEW_PARAMETER, out value) && value is bool isNew)
            {
                SetIsNew(isNew);
            }
            await base.ApplyQueryAttributesAsync(query);
        }

        private async Task SetValue(int id)
        {
            (bool succeeded, T value) = await Service.TryGetAsync(id);
            if (succeeded)
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
    }
}