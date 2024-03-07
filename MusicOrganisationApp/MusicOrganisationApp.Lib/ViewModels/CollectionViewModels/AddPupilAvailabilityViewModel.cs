using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AddPupilAvailabilityViewModel : CollectionViewModelBase<LessonSlot>, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(AddPupilAvailabilityViewModel);

        private readonly PupilAvailabilityService _service;

        [ObservableProperty]
        private LessonSlot? _selectedLessonSlot;

        public AddPupilAvailabilityViewModel()
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;


        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        async partial void OnSelectedLessonSlotChanged(LessonSlot? value)
        {
            if (value is not null)
            {
                await _service.AddAvailabilityAsync(value);
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

        protected override async Task<IEnumerable<LessonSlot>> GetAllAsync()
        {
            IEnumerable<LessonSlot> lessonSlots = await _service.GetUnusedLessonSlotsAsync();
            return lessonSlots;
        }
    }
}