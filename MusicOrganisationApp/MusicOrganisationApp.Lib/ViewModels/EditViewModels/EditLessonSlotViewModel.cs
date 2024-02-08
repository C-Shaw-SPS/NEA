using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditLessonSlotViewModel : EditLessonViewModelBase<LessonSlotData, LessonSlotData>, IQueryAttributable
    {
        public const string ROUTE = nameof(EditLessonSlotViewModel);

        private const string _EDIT_PAGE_TITLE = "Edit lesson slot";
        private const string _NEW_PAGE_TITLE = "New lesson slot";

        private static readonly List<DayOfWeek> _daysOfWeek = LessonSlotService.GetDaysOfWeek();

        private readonly LessonSlotService _service;

        [ObservableProperty]
        private DayOfWeek _dayOfWeek = DayOfWeek.Sunday;

        public EditLessonSlotViewModel() : base(LessonSlotViewModel.ROUTE, _EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

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