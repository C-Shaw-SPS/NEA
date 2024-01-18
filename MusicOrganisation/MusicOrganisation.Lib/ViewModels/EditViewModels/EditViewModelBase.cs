﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Viewmodels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisation.Lib.ViewModels.EditViewModels
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);
    }

    public abstract partial class EditViewModelBase<TModel, TModelViewModel> : EditViewModelBase, IQueryAttributable
        where TModel : class, ITable, new()
        where TModelViewModel : ModelViewModelBase, IViewModel
    {
        private int _id;
        private bool _isNew;
        private readonly string _newPageTitle;
        private readonly string _editPageTitle;

        protected TModel _value;

        [ObservableProperty]
        private string _pageTitle;

        [ObservableProperty]
        private bool _canDelete;

        private readonly AsyncRelayCommand _saveCommand;
        private readonly AsyncRelayCommand _deleteCommand;

        public EditViewModelBase(string newPageTitle, string editPageTitle)
        {
            _newPageTitle = newPageTitle;
            _editPageTitle = editPageTitle;

            _pageTitle = _editPageTitle;
            _canDelete = true;

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
            _value.Id = _id;
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
                [ModelViewModelBase.ID_PARAMETER] = _id
            };
            await GoToAsync(parameters, _RETURN, TModelViewModel.Route);
        }

        private async Task SaveExistingAsync()
        {
            await _service.UpdateAsync(_value);
            await GoToAsync(_RETURN);
        }

        #endregion

        private async Task DeleteAsync()
        {
            await _service.DeleteAsync(_value);
            await GoToAsync(_RETURN, _RETURN);
        }

        #region Page Setup

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                _id = id;
                _value = await _service.GetAsync<TModel>(id);
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
            _id = await _service.GetNextIdAsync<TModel>();
            PageTitle = _newPageTitle;
        }

        #endregion
    }
}