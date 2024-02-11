using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AddPupilAvailabilityViewModel : CollectionViewModelBase<LessonSlotData>, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(AddPupilAvailabilityViewModel);

        private readonly PupilAvailabilityService _service;
        private readonly AsyncRelayCommand _selectCommand;

        [ObservableProperty]
        private ObservableCollection<LessonSlotData> _lessonSlots = [];

        [ObservableProperty]
        private LessonSlotData? _selectedLessonSlot;

        public AddPupilAvailabilityViewModel()
        {
            _service = new(_database);
            _selectCommand = new(SelectAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand SelectCommand => _selectCommand;

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        private async Task SelectAsync()
        {
            if (SelectedLessonSlot is not null)
            {
                await _service.AddAvailabilityAsync(SelectedLessonSlot);
                await RefreshAsync();
            }
        }

        protected override async Task AddNewAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditViewModelBase.IS_NEW_PARAMETER] = true
            };
            await GoToAsync<EditLessonSlotViewModel>(parameters);
        }

        protected override async Task<IEnumerable<LessonSlotData>> GetAllAsync()
        {
            IEnumerable<LessonSlotData> lessonSlots = await _service.GetUnusedLessonSlotsAsync();
            return lessonSlots;
        }
    }
}