using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class LessonSlotViewModel : ModelViewModelBase<LessonSlot, EditLessonSlotViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(LessonSlotViewModel);

        private readonly LessonSlotService _service;

        [ObservableProperty]
        private string _dayOfWeek = string.Empty;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        public LessonSlotViewModel() : base(GetDefaultPath(), false)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<LessonSlot> Service => _service;

        protected override void SetDisplayValues()
        {
            DayOfWeek = _value.DayOfWeek.ToString();
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
        }
    }
}