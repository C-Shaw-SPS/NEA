﻿using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public class DeveloperToolsViewModel : ViewModelBase
    {
        private const string _ROUTE = nameof(DeveloperToolsViewModel);

        private readonly AsyncRelayCommand _dropTablesCommand;
        private readonly AsyncRelayCommand _resetComposersAndWorksCommand;

        public DeveloperToolsViewModel()
        {
            _dropTablesCommand = new(DropTablesAsync);
            _resetComposersAndWorksCommand = new(ResetComposersAndWorksAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand DropTablesCommand => _dropTablesCommand;

        public AsyncRelayCommand ResetComposersAndWorksCommand => _resetComposersAndWorksCommand;

        private async Task DropTablesAsync()
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

        private async Task ResetComposersAndWorksAsync()
        {
            IEnumerable<ComposerData> composers = await ComposerGetter.GetFromOpenOpus();
            IEnumerable<WorkData> workData = await WorkGetter.GetFromOpenOpus();

            await _database.ResetTableAsync(composers);
            await _database.ResetTableAsync(workData);
        }
    }
}