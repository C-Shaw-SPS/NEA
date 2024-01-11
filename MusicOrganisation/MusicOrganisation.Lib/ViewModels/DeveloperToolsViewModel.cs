using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase
    {
        public const string PATH = nameof(DeveloperToolsViewModel);

        private readonly Service _service;

        private readonly RelayCommand _deleteDatabaseCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;

        public DeveloperToolsViewModel()
        {
            _service = new(_databasePath);
            _deleteDatabaseCommand = new(DeleteDatabase);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorks);
        }

        public RelayCommand DeleteDatabaseCommand => _deleteDatabaseCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        private void DeleteDatabase()
        {
            File.Delete(_databasePath);
        }

        private async Task ResetComposersAndWorks()
        {
            Task<IEnumerable<ComposerData>> composerTask = ComposerGetter.GetFromOpenOpus();
            Task<IEnumerable<WorkData>> workTask = WorkGetter.GetFromOpenOpus();

            await Task.WhenAll(
                _service.ClearTableAsync<ComposerData>(),
                _service.ClearTableAsync<WorkData>(),
                composerTask,
                workTask
                );

            IEnumerable<ComposerData> composers = composerTask.Result;
            IEnumerable<WorkData> works = workTask.Result;

            await Task.WhenAll(
                _service.InsertAllAsync(composers),
                _service.InsertAllAsync(works)
                );
        }
    }
}