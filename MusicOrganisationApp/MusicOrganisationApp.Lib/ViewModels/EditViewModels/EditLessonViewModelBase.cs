using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditLessonViewModelBase<T> : EditViewModelBase<T> where T : class, ILesson, ITable, new()
    {
        private const string _END_BEFORE_START_ERROR = "End time cannot be before start time";
        private const string _CLASH_ERROR = "Lesson clashes";

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private string _timeError = string.Empty;

        [ObservableProperty]
        private ObservableCollection<T> _clashingLessons = [];

        protected EditLessonViewModelBase(string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {
        }

        protected abstract LessonServiceBase<T> LessonService { get; }

        protected abstract object SelectedDateObject { get; }

        protected override IService<T> Service => LessonService;

        protected override void SetDisplayValues()
        {
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
        }

        protected override async Task<bool> TrySetValuesToSave()
        {
            SetDateObjectToSave();
            bool canSave = await TrySetTimesToSave();
            return canSave;
        }

        protected abstract void SetDateObjectToSave();

        private async Task<bool> TrySetTimesToSave()
        {
            if (EndTime < StartTime)
            {
                TimeError = _END_BEFORE_START_ERROR;
                return false;
            }
            int? id = _isNew ? null : _value.Id;
            IEnumerable<T> clashingLessons = await LessonService.GetClashingLessonsAsync(SelectedDateObject, StartTime, EndTime, id);
            if (clashingLessons.Any())
            {
                TimeError = _CLASH_ERROR;
                ResetCollection(ClashingLessons, clashingLessons);
                return false;
            }
            else
            {
                TimeError = string.Empty;
                ClashingLessons.Clear();
                _value.StartTime = StartTime;
                _value.EndTime = EndTime;
                return true;
            }
        }
    }
}