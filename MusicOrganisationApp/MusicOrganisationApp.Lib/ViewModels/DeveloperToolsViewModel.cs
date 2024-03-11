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

        public DeveloperToolsViewModel() : base(GetDefaultPath(), false)
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
                _database.DropTableIfExistsAsync<Composer>(),
                _database.DropTableIfExistsAsync<LessonData>(),
                _database.DropTableIfExistsAsync<LessonSlot>(),
                _database.DropTableIfExistsAsync<Pupil>(),
                _database.DropTableIfExistsAsync<PupilAvailability>(),
                _database.DropTableIfExistsAsync<RepertoireData>(),
                _database.DropTableIfExistsAsync<WorkData>()
            );
        }

        private async Task ResetComposersAndWorksAsync()
        {
            IEnumerable<Composer> composers = await ComposerGetter.GetFromOpenOpusAsync();
            IEnumerable<WorkData> workData = await WorkGetter.GetFromOpenOpusAsync();

            await _database.ResetTableAsync(composers, false);
            await _database.ResetTableAsync(workData, false);
        }

        private async Task InsertTestDataAsync()
        {
            await DropTablesAsync();
            await _database.InsertAllAsync(TestData1.Pupils, false);
            await _database.InsertAllAsync(TestData1.Lessons, false);
            await _database.InsertAllAsync(TestData1.LessonSlots, false);
            await _database.InsertAllAsync(TestData1.PupilAvailabilities, false);
        }
    }
}