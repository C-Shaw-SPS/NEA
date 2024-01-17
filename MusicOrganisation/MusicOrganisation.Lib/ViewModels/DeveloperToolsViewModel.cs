using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Models;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase
    {
        public const string ROUTE = nameof(DeveloperToolsViewModel);

        private readonly AsyncRelayCommand _deleteDatabaseCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;

        public DeveloperToolsViewModel()
        {
            _deleteDatabaseCommand = new(DeleteDatabaseTables);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorks);
        }

        public AsyncRelayCommand DeleteDatabaseCommand => _deleteDatabaseCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        private async Task DeleteDatabaseTables()
        {
            await Task.WhenAll(
                _service.DropTableIfExistsAsync<Pupil>(),
                _service.DropTableIfExistsAsync<CaregiverData>(),
                _service.DropTableIfExistsAsync<CaregiverMap>(),
                _service.DropTableIfExistsAsync<ComposerData>(),
                _service.DropTableIfExistsAsync<LessonData>(),
                _service.DropTableIfExistsAsync<LessonSlotData>(),
                _service.DropTableIfExistsAsync<RepertoireData>(),
                _service.DropTableIfExistsAsync<WorkData>()
                );
        }

        private async Task ResetComposersAndWorks()
        {
            Task<IEnumerable<ComposerData>> composerTask = ComposerGetter.GetFromOpenOpus();
            Task<IEnumerable<WorkData>> workTask = WorkGetter.GetFromOpenOpus();

            await Task.WhenAll(
                _service.DropTableIfExistsAsync<ComposerData>(),
                _service.DropTableIfExistsAsync<WorkData>(),
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