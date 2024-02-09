using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.TestData;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase, IViewModel
    {
        private const string _ROUTE = nameof(DeveloperToolsViewModel);

        private readonly AsyncRelayCommand _dropTablesCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;
        private readonly AsyncRelayCommand _insertTestDataCommand;

        public DeveloperToolsViewModel()
        {
            _dropTablesCommand = new(DropTablesAsync);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorksAsync);
            _insertTestDataCommand = new(InsertTestDataAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand DropTablesCommand => _dropTablesCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        public AsyncRelayCommand InsertTestDataCommand => _insertTestDataCommand;

        private async Task DropTablesAsync()
        {
            await Task.WhenAll(
                _database.DropTableIfExistsAsync<CaregiverData>(),
                _database.DropTableIfExistsAsync<CaregiverMap>(),
                _database.DropTableIfExistsAsync<ComposerData>(),
                _database.DropTableIfExistsAsync<LessonData>(),
                _database.DropTableIfExistsAsync<LessonSlotData>(),
                _database.DropTableIfExistsAsync<Pupil>(),
                _database.DropTableIfExistsAsync<PupilAvailability>(),
                _database.DropTableIfExistsAsync<RepertoireData>(),
                _database.DropTableIfExistsAsync<WorkData>()
            );
        }

        private async Task ResetComposersAndWorksAsync()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            IEnumerable<WorkData> workData = await WorkGetter.GetFromOpenOpus();

            await _database.ResetTableAsync(composers);
            await _database.ResetTableAsync(workData);
        }

        private async Task InsertTestDataAsync()
        {
            await DropTablesAsync();
            await _database.InsertAllAsync(TestData1.Pupils);
            await _database.InsertAllAsync(TestData1.Lessons);
            await _database.InsertAllAsync(TestData1.LessonSlots);
            await _database.InsertAllAsync(TestData1.PupilAvailabilities);
        }
    }
}