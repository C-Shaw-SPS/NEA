﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Services;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class SearchableEditViewModel<TModel, TSearch, TEditSearchViewModel> : EditViewModelBase<TModel>
        where TModel : class, IIdentifiable, new()
        where TSearch : class, IIdentifiable, new()
        where TEditSearchViewModel : IViewModel
    {
        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _addNewSearchItemCommand;
        private readonly string _noItemSeletedError;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<TSearch> _searchResult = [];

        [ObservableProperty]
        private TSearch? _selectedItem;

        [ObservableProperty]
        private string _selectedItemText = string.Empty;

        [ObservableProperty]
        private string _searchError = string.Empty;

        public SearchableEditViewModel(string editPageTitle, string newPageTitle, string noItemSelectedError, string path, bool isTesting) : base(editPageTitle, newPageTitle, path, isTesting)
        {
            _searchCommand = new(SearchAsync);
            _addNewSearchItemCommand = new(AddNewSearchItemAsync);
            _noItemSeletedError = noItemSelectedError;
        }

        protected abstract ISearchService<TSearch> SearchService { get; }

        protected abstract string SearchOrdering { get; }

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand AddNewSearchItemCommand => _addNewSearchItemCommand;

        private async Task SearchAsync()
        {
            IEnumerable<TSearch> searchResult = await SearchService.SearchAsync(SearchText, SearchOrdering);
            ResetCollection(searchResult);
        }

        private void ResetCollection(IEnumerable<TSearch> values)
        {
            SearchResult.Clear();
            foreach (TSearch value in values)
            {
                SearchResult.Add(value);
            }
        }

        private async Task AddNewSearchItemAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [IS_NEW_PARAMETER] = true
            };
            await GoToAsync<TEditSearchViewModel>(parameters);
        }

        partial void OnSelectedItemChanged(TSearch? value)
        {
            if (value is not null)
            {
                UpdateSelectedItemText(value);
            }
        }

        protected abstract void UpdateSelectedItemText(TSearch value);

        protected override Task<bool> TrySetValuesToSaveAsync()
        {
            bool canSave = true;
            canSave &= TrySetSearchValueToSave();
            canSave &= TrySetNonSearchValuesToSave();
            return Task.FromResult(canSave);
        }

        private bool TrySetSearchValueToSave()
        {
            if (SelectedItem is TSearch selectedItem)
            {
                SetSearchValuesToSave(selectedItem);
                SearchError = string.Empty;
                return true;
            }
            else if (_isNew)
            {
                SearchError = _noItemSeletedError;
                return false;
            }
            else
            {
                SearchError = string.Empty;
                return true;
            }
        }

        protected abstract void SetSearchValuesToSave(TSearch selectedItem);

        protected abstract bool TrySetNonSearchValuesToSave();
    }
}