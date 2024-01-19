using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Json;
using MusicOrganisation.Lib.Models;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase, IViewModel
    {
        private const string _ROUTE = nameof(DeveloperToolsViewModel);

        private readonly AsyncRelayCommand _deleteDatabaseCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;

        public DeveloperToolsViewModel()
        {
            _deleteDatabaseCommand = new(DeleteDatabaseTables);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorks);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand DeleteDatabaseCommand => _deleteDatabaseCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        private async Task DeleteDatabaseTables()
        {
            await Task.WhenAll(
                _database.DropTableIfExistsAsync<Pupil>(),
                _database.DropTableIfExistsAsync<CaregiverData>(),
                _database.DropTableIfExistsAsync<CaregiverMap>(),
                _database.DropTableIfExistsAsync<ComposerData>(),
                _database.DropTableIfExistsAsync<LessonData>(),
                _database.DropTableIfExistsAsync<LessonSlotData>(),
                _database.DropTableIfExistsAsync<RepertoireData>(),
                _database.DropTableIfExistsAsync<WorkData>()
                );
        }

        private async Task ResetComposersAndWorks()
        {
            Task<IEnumerable<ComposerData>> composerTask = ComposerGetter.GetFromOpenOpus();
            Task<IEnumerable<WorkData>> workTask = WorkGetter.GetFromOpenOpus();

            await Task.WhenAll(
                _database.DropTableIfExistsAsync<ComposerData>(),
                _database.DropTableIfExistsAsync<WorkData>(),
                composerTask,
                workTask
                );

            IEnumerable<ComposerData> composers = composerTask.Result;
            IEnumerable<WorkData> works = workTask.Result;

            await Task.WhenAll(
                _database.InsertAllAsync(composers),
                _database.InsertAllAsync(works)
                );
        }
    }
}