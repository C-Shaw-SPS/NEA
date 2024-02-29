using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using MusicOrganisationApp.Lib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<T> : ViewModelBase where T : class, IIdentifiable, new()
    {
        private readonly AsyncRelayCommand _addNewCommand;

        [ObservableProperty]
        private ObservableCollection<T> _collection = [];

        [ObservableProperty]
        private T? _selectedItem;

        public CollectionViewModelBase()
        {
            _addNewCommand = new(AddNewAsync);
        }

        public CollectionViewModelBase(string path, bool isTesting) : base(path, isTesting)
        {
            _addNewCommand = new(AddNewAsync);
        }

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        protected abstract Task AddNewAsync();

        protected abstract Task<IEnumerable<T>> GetAllAsync();

        public async Task RefreshAsync()
        {
            IEnumerable<T> values = await GetAllAsync();
            IViewModel.ResetCollection(Collection, values);
        }
    }
}