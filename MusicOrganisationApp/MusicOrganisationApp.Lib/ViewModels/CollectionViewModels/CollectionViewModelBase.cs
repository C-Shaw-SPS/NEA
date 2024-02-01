﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<T> : ViewModelBase where T : class, IIdentifiable, new()
    {
        private readonly string _modelRoute;
        private readonly string _editRoute;
        private readonly Dictionary<string, string> _orderings;
        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _addNewCommand;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedOrdering;

        [ObservableProperty]
        private ObservableCollection<T> _collection = [];

        [ObservableProperty]
        private T? _selectedItem;

        public CollectionViewModelBase(string modelRoute, string editRoute, Dictionary<string, string> orderings)
        {
            _modelRoute = modelRoute;
            _editRoute = editRoute;
            _orderings = orderings;

            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);
            _addNewCommand = new(AddNewAsync);

            _selectedOrdering = Orderings.First();
        }

        protected abstract IService<T> Service { get; }

        public List<string> Orderings => _orderings.Keys.ToList();

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public virtual async Task RefreshAsync()
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            string ordering = _orderings[SelectedOrdering];
            IEnumerable<T> values = await Service.SearchAsync(SearchText, ordering);
            Collection.Clear();
            foreach (T value in values)
            {
                Collection.Add(value);
            }
        }

        private async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> paramters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedItem.Id
                };
                await GoToAsync(paramters, _modelRoute);
            }
        }

        private async Task AddNewAsync()
        {
            Dictionary<string, object> paramters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            AddAddNewParameters(paramters);
            await GoToAsync(paramters, _editRoute);
        }

        protected virtual void AddAddNewParameters(Dictionary<string, object> parameters) { }
    }
}