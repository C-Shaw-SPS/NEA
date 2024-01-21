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
        private static Dictionary<string, string> _orderings = new()
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

        private AsyncRelayCommand _searchCommand;
        private AsyncRelayCommand _selectCommand;
        private AsyncRelayCommand _newCommand;

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
            SqlQuery<ComposerData> sqlQuery = new(SqlQuery.DEFAULT_LIMIT);
            sqlQuery.SetSelectAll();
            sqlQuery.AddWhereLike<ComposerData>(nameof(ComposerData.Name), SearchText);
            sqlQuery.AddOrderBy<ComposerData>(ordering);
            throw new NotImplementedException();
        }
    }
}