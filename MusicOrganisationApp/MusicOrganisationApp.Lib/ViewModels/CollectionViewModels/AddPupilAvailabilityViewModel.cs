using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AddPupilAvailabilityViewModel : ViewModelBase, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(AddPupilAvailabilityViewModel);

        private readonly PupilAvailabilityService _service;
        private readonly AsyncRelayCommand _selectCommand;
        private readonly AsyncRelayCommand _addNewCommand;

        [ObservableProperty]
        private ObservableCollection<LessonSlotData> _lessonSlots = [];

        [ObservableProperty]
        private LessonSlotData? _selectedLessonSlot;

        public AddPupilAvailabilityViewModel()
        {
            _service = new(_database);
            _selectCommand = new(SelectAsync);
            _addNewCommand = new(AddNewAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        public async Task RefreshAsync()
        {
            IEnumerable<LessonSlotData> lessonSlots = await _service.GetUnusedLessonSlotsAsync();
            IViewModel.ResetCollection(LessonSlots, lessonSlots);
        }

        private async Task SelectAsync()
        {
            if (SelectedLessonSlot is not null)
            {
                await _service.AddAvailabilityAsync(SelectedLessonSlot);
                await RefreshAsync();
            }
        }

        private async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            await GoToAsync<EditLessonSlotViewModel>(parameters);
        }
    }
}