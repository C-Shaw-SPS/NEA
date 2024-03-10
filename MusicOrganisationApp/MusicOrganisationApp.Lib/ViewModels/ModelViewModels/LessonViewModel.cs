using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class LessonViewModel : ModelViewModelBase<Lesson, EditLessonViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(LessonViewModel);

        private readonly LessonService _service;

        [ObservableProperty]
        private string _pupilName = string.Empty;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private string _notes = string.Empty;

        public LessonViewModel() : base(GetDefaultPath(), false)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Lesson> Service => _service;

        protected override void SetDisplayValues()
        {
            PupilName = _value.PupilName;
            Date = _value.Date;
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
            Notes = _value.Notes;
        }

        protected override void AddEditRouteParameters(Dictionary<string, object> parameters)
        {
            parameters[IPupilDataViewModel.PUPIL_ID_PARAMETER] = _value.PupilId;
        }
    }
}