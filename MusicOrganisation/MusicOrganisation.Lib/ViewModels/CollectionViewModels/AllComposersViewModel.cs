using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System.Collections.ObjectModel;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(ComposerData.Name),
            ["Year of birth"] = nameof(ComposerData.BirthYear),
            ["Year of death"] = nameof(ComposerData.DeathYear)
        };

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _selectedOrdering;

        [ObservableProperty]
        private ComposerData? _selectedItem;

        [ObservableProperty]
        private ObservableCollection<ComposerData> _collection;

        private readonly AsyncRelayCommand _searchCommand;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _newCommand;

        public AllComposersViewModel()
        {
            _collection = [];
            _searchCommand = new(SearchAsync);
        }

        public AsyncRelayCommand SearchCommand => _searchCommand;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand NewCommand => _newCommand;

        public async Task SearchAsync()
        {
            string ordering = _orderings[SelectedOrdering];
            SelectQuery<ComposerData> sqlQuery = new(SelectQuery.DEFAULT_LIMIT);
            sqlQuery.SetSelectAll();
            sqlQuery.AddWhereLike<ComposerData>(nameof(ComposerData.Name), SearchText);
            sqlQuery.AddOrderBy<ComposerData>(ordering);

            throw new NotImplementedException();
        }
    }
}