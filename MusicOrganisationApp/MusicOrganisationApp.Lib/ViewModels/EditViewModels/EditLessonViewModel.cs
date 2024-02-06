using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditLessonViewModel : EditViewModelBase<LessonData>
    {
        private const string _EDIT_PAGE_TITLE = "Edit lesson";
        private const string _NEW_PAGE_TITLE = "New lesson";

        private readonly LessonService _service;

        [ObservableProperty]
        private DateTime _date = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private ObservableCollection<LessonData> _clashingLessonSlots = [];

        [ObservableProperty]
        private LessonData? _selectedClashingLessonSlot;

        public EditLessonViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
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

        protected override Task<bool> TrySetValuesToSave()
        {
            throw new NotImplementedException();
        }
    }
}