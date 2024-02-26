﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class SelectableCollectionViewModel<TModel, TModelViewModel, TEditViewModel> : CollectionViewModelBase<TModel>
        where TModel : class, IIdentifiable, new()
        where TModelViewModel : IViewModel
        where TEditViewModel : IViewModel
    {
        private readonly AsyncRelayCommand _selectCommand;

        [ObservableProperty]
        private TModel? _selectedItem;

        public SelectableCollectionViewModel()
        {
            _selectCommand = new(SelectAsync);
        }

        protected abstract IService<TModel> Service { get; }

        public AsyncRelayCommand SelectCommand => _selectCommand;

        private async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> paramters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedItem.Id
                };
                await GoToAsync<TModelViewModel>(paramters);
            }
        }

        protected override async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            await GoToAsync<TEditViewModel>(parameters);
        }

        protected override async Task<IEnumerable<TModel>> GetAllAsync()
        {
            IEnumerable<TModel> values = await Service.GetAllAsync();
            return values;
        }
    }
}