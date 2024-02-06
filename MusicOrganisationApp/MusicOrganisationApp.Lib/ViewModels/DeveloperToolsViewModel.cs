using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase
    {
        public const string ROUTE = nameof(DeveloperToolsViewModel);

        private readonly AsyncRelayCommand _dropTablesCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;

        public DeveloperToolsViewModel()
        {
            _dropTablesCommand = new(DropTablesAsync);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorksAsync);
        }

        public AsyncRelayCommand DropTablesCommand => _dropTablesCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        private async Task DropTablesAsync()
        {
            await Task.WhenAll( _database.DropTableIfExistsAsync<Pupil>(),
                _database.DropTableIfExistsAsync<CaregiverData>(),
                _database.DropTableIfExistsAsync<ComposerData>(),
                _database.DropTableIfExistsAsync<LessonData>(),
                _database.DropTableIfExistsAsync<LessonSlotData>(),
                _database.DropTableIfExistsAsync<RepertoireData>(),
                _database.DropTableIfExistsAsync<WorkData>(),
                _database.DropTableIfExistsAsync<CaregiverMap>()
            );
        }

        private async Task ResetComposersAndWorksAsync()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            IEnumerable<WorkData> workData = await WorkGetter.GetFromOpenOpus();

            await _database.ResetTableAsync(composers);
            await _database.ResetTableAsync(workData);
        }
    }
}