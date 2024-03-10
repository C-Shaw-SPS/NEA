using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Databases;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<T> : ViewModelBase where T : class, IIdentifiable, new()
    {
        private readonly AsyncRelayCommand _addNewCommand;

        [ObservableProperty]
        private ObservableCollection<T> _collection = [];

        [ObservableProperty]
        private T? _selectedItem;

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
            ResetCollection(Collection, values);
        }
    }
}