using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Viewmodels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisation.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditViewModelBase<T> : ViewModelBase, IQueryAttributable where T : class, ITable, new()
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);

        private readonly Service _service;
        protected T _value;
        private int _id;
        private bool _isNew;

        private readonly string _newPageTitle;
        private readonly string _editPageTitle;
        private readonly string _modelViewModelRoute;

        [ObservableProperty]
        private string _pageTitle;

        [ObservableProperty]
        private bool _canDelete;

        private AsyncRelayCommand _saveCommand;
        private AsyncRelayCommand _deleteCommand;

        public EditViewModelBase(string newPageTitle, string editPageTitle, string modelViewModelRoute)
        {
            _newPageTitle = newPageTitle;
            _editPageTitle = editPageTitle;
            _modelViewModelRoute = modelViewModelRoute;

            _pageTitle = _newPageTitle;
            _canDelete = true;

            _service = new(_databasePath);
            _value = new();
            _isNew = false;

            _saveCommand = new(TrySaveAsync);
            _deleteCommand = new(DeleteAsync);
        }

        public AsyncRelayCommand SaveCommand => _saveCommand;

        public AsyncRelayCommand DeleteCommand => _deleteCommand;

        #region Saving

        private async Task TrySaveAsync()
        {
            bool canSave = TrySetValuesToSave();
            if (canSave)
            {
                if (_isNew)
                {
                    await SaveNewAsync();
                }
                else
                {
                    await SaveExistingAsync();
                }
            }
        }

        protected abstract bool TrySetValuesToSave();

        private async Task SaveNewAsync()
        {
            _isNew = false;
            CanDelete = true;
            await _service.InsertAsync(_value);
            Dictionary<string, object> parameters = new()
            {
                [ModelViewModelBase<T>.ID_PARAMETER] = _id
            };
            await GoToAsync(parameters, _RETURN, _modelViewModelRoute);
        }

        private async Task SaveExistingAsync()
        {
            await _service.UpdateAsync(_value);
            await GoToAsync(_RETURN);
        }

        #endregion

        protected abstract Task DeleteAsync();

        #region Page Setup

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                _value = await _service.GetAsync<T>(id);
                SetDisplayValues();
            }
            if (query.TryGetValue(IS_NEW_PARAMETER, out value) && value is bool isNew && isNew)
            {
                await SetCreateNewAsync();
            }
        }

        protected abstract void SetDisplayValues();

        private async Task SetCreateNewAsync()
        {
            _isNew = true;
            CanDelete = false;
            _id = await _service.GetNextIdAsync<T>();
            PageTitle = _editPageTitle;
        }

        #endregion
    }
}