using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Viewmodels;
using System.Collections.ObjectModel;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    public abstract partial class CollectionViewModelBase<T> : ViewModelBase
    {
        private const int _LIMIT = 128;

        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _newCommand;
        private readonly Dictionary<string, string> _orderings;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _ordering;

        [ObservableProperty]
        private ObservableCollection<T> _collection;

        [ObservableProperty]
        private T? _selectedItem;

        public CollectionViewModelBase(Dictionary<string, string> orderings)
        {
            _searchCommand = new(SearchAsync);
            _selectCommand = new(SelectAsync);
            _newCommand = new(AddNewAsync);

            _orderings = orderings;

            _searchText = string.Empty;
            _ordering = _orderings.Keys.First();
            _collection = [];
        }

        public List<string> Orderings => new(_orderings.Keys);

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand NewCommand => _newCommand;

        protected abstract Task SearchAsync();

        protected abstract Task SelectAsync();

        protected abstract Task AddNewAsync();
    }
}