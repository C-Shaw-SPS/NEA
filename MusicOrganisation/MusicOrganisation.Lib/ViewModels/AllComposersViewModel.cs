using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System.IO;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        public const string ROUTE = nameof(AllComposersViewModel);

        private ComposerService _composerService;
        private string _path;

        private AsyncRelayCommand _refreshCommand;
        private AsyncRelayCommand _initialiseCommand;


        [ObservableProperty]
        private List<ComposerData> _composers;

        public AllComposersViewModel()
        {
            _path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _composerService = new(_path);
            _composers = [];
            _refreshCommand = new(RefreshAsync);
            _initialiseCommand = new(InitialiseAsync);
        }

        public AsyncRelayCommand RefreshCommand => _refreshCommand;

        public AsyncRelayCommand InitialiseCommand => _initialiseCommand;

        public async Task RefreshAsync()
        {
            IEnumerable<ComposerData> composers = await _composerService.GetAllAsync<ComposerData>();
            Composers.AddRange(composers);
        }

        private async Task InitialiseAsync()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            await _composerService.ClearTableAsync<ComposerData>();
            await _composerService.InsertAllAsync<ComposerData>(composers);
            await RefreshAsync();
        }
    }
}