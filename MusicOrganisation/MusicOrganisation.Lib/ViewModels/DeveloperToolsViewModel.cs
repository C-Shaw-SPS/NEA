using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Models;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase
    {
        public const string ROUTE = nameof(DeveloperToolsViewModel);

        private readonly Service _service;

        private readonly AsyncRelayCommand _deleteDatabaseCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;

        public DeveloperToolsViewModel()
        {
            _service = new(_databasePath);
            _deleteDatabaseCommand = new(DeleteDatabaseTables);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorks);
        }

        public AsyncRelayCommand DeleteDatabaseCommand => _deleteDatabaseCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        private async Task DeleteDatabaseTables()
        {
            await Task.WhenAll(
                _service.DropTableIfExists<Pupil>(),
                _service.DropTableIfExists<CaregiverData>(),
                _service.DropTableIfExists<CaregiverMap>(),
                _service.DropTableIfExists<ComposerData>(),
                _service.DropTableIfExists<LessonData>(),
                _service.DropTableIfExists<LessonSlotData>(),
                _service.DropTableIfExists<RepertoireData>(),
                _service.DropTableIfExists<WorkData>()
                );
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