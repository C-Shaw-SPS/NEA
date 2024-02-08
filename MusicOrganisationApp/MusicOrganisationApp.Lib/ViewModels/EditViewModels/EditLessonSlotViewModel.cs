using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditLessonSlotViewModel : EditLessonViewModelBase<LessonSlotData, LessonSlotData, LessonSlotViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditLessonSlotViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit lesson slot";
        private const string _NEW_PAGE_TITLE = "New lesson slot";

        private static readonly List<DayOfWeek> _daysOfWeek = LessonSlotService.GetDaysOfWeek();

        private readonly LessonSlotService _service;

        [ObservableProperty]
        private DayOfWeek _dayOfWeek = DayOfWeek.Sunday;

        public EditLessonSlotViewModel() : base(LessonSlotViewModel.Route, _EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        public List<DayOfWeek> DaysOfWeek => _daysOfWeek;

        protected override LessonServiceBase<LessonSlotData, LessonSlotData> LessonService => _service;

        protected override object SelectedDateObject => DayOfWeek;

        partial void OnDayOfWeekChanged(DayOfWeek value)
        {
            _service.DayOfWeek = value;
        }

        protected override bool TrySetNonTimeValuesToSave()
        {
            _value.DayOfWeek = DayOfWeek;
            return true;
        }

        protected override void SetDisplayNonTimeValues()
        {
            DayOfWeek = _value.DayOfWeek;
        }
    }
}