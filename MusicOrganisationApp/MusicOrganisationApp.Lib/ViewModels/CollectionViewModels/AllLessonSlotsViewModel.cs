using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllLessonSlotsViewModel : CollectionViewModelBase<LessonSlotData>
    {
        public const string ROUTE = nameof(AllLessonSlotsViewModel);

        private static readonly List<DayOfWeek> _daysOfWeek = LessonSlotService.GetDaysOfWeek();

        private readonly LessonSlotService _service;

        [ObservableProperty]
        private DayOfWeek _selectedDayOfWeek = DayOfWeek.Monday;

        public AllLessonSlotsViewModel() : base(LessonSlotViewModel.ROUTE, EditLessonSlotViewModel.ROUTE)
        {
            _service = new(_database);
        }

        public List<DayOfWeek> DaysOfWeek => _daysOfWeek;

        protected override IService<LessonSlotData> Service => _service;

        async partial void OnSelectedDayOfWeekChanged(DayOfWeek value)
        {
            _service.DayOfWeek = value;
            await RefreshAsync();
        }
    }
}