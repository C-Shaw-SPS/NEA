using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System.IO;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private ComposerService _composerService;

        [ObservableProperty]
        private List<ComposerData> _composers;

        public const string ROUTE = nameof(AllComposersViewModel);

        private string _path;

        public AllComposersViewModel()
        {
            _path = Path.Combine(FileSystem.AppDataDirectory, DatabaseProperties.NAME);
            _composerService = new(_path);
            _composers = [];
        }

        public async Task Refresh()
        {
            IEnumerable<ComposerData> composers = await _composerService.GetAllAsync<ComposerData>();
            Composers.AddRange(composers);
        }
    }
}