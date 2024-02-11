using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllLessonSlotsViewModel : SelectableCollectionViewModel<LessonSlotData, LessonSlotViewModel, EditLessonSlotViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllLessonSlotsViewModel);

        private static readonly List<DayOfWeek> _daysOfWeek = LessonSlotService.GetDaysOfWeek();

        private readonly LessonSlotService _service;

        [ObservableProperty]
        private DayOfWeek _selectedDayOfWeek = DayOfWeek.Sunday;

        public AllLessonSlotsViewModel() : base()
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        public List<DayOfWeek> DaysOfWeek => _daysOfWeek;

        protected override IService<LessonSlotData> Service => _service;

        async partial void OnSelectedDayOfWeekChanged(DayOfWeek value)
        {
            _service.DayOfWeek = value;
            await RefreshAsync();
        }
    }
}