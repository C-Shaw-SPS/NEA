using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class LessonViewModel : ModelViewModelBase<LessonData>, IQueryAttributable
    {
        public const string ROUTE = nameof(LessonViewModel);

        private readonly LessonService _service;

        [ObservableProperty]
        private DateTime _date;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private string _notes = string.Empty;

        public LessonViewModel() : base(EditLessonViewModel.ROUTE)
        {
            _service = new(_database);
        }

        protected override IService<LessonData> Service => _service;

        protected override void SetDisplayValues()
        {
            Date = _value.Date;
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
            Notes = _value.Notes;
        }
    }
}