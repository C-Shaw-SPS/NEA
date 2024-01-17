﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Viewmodels;
using MusicOrganisation.Lib.ViewModels.EditViewModels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<TModel, TModelViewModel, TEditViewModel> : ViewModelBase 
        where TModel : class, ITable, new()
        where TModelViewModel : ModelViewModelBase, IViewModel
        where TEditViewModel : EditViewModelBase, IViewModel
    {
        protected const int _LIMIT = 256;

        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _newCommand;
        private readonly Dictionary<string, string> _orderings;
        private readonly string _searchParameter;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _ordering;

        [ObservableProperty]
        private ObservableCollection<TModel> _collection;

        [ObservableProperty]
        private TModel? _selectedItem;

        public CollectionViewModelBase(Dictionary<string, string> orderings, string searchParameter)
        {
            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);
            _newCommand = new(AddNewAsync);

            _orderings = orderings;
            _searchParameter = searchParameter;

            _searchText = string.Empty;
            _ordering = _orderings.Keys.First();
            _collection = [];
        }

        public List<string> Orderings => new(_orderings.Keys);

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand NewCommand => _newCommand;

        public async Task RefreshAsync()
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            string ordering = _orderings[Ordering];
            SqlQuery<TModel> query = new();
            query.SetSelectAll();
            query.AddWhereLike<TModel>(_searchParameter, SearchText);
            query.AddOrderBy<TModel>(ordering);
            query.SetLimit(_LIMIT);

            string queryString = query.ToString();
            IEnumerable<TModel> values = await _service.QueryAsync<TModel>(queryString);

            Collection.Clear();
            foreach (TModel value in values)
            {
                Collection.Add(value);
            }
        }

        private async Task SelectAsync()
        {
            if (SelectedItem is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedItem.Id,
                };
                await GoToAsync(parameters, TModelViewModel.Route);
            }
        }

        private async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            await GoToAsync(parameters, TEditViewModel.Route);
        }
    }
}