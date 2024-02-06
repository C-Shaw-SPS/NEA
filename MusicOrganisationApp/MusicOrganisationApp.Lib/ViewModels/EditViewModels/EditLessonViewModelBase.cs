using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditLessonViewModelBase<T> : EditViewModelBase<T>, IQueryAttributable where T : class, ILesson, ITable, new()
    {
        private const string _END_BEFORE_START_ERROR = "End time cannot be before start time";
        private const string _CLASH_ERROR = "Lesson clashes";

        private readonly string _modelRoute;
        private readonly AsyncRelayCommand _goToClashingLessonCommand;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private string _timeError = string.Empty;

        [ObservableProperty]
        private ObservableCollection<T> _clashingLessons = [];

        [ObservableProperty]
        private T? _selectedClashingLesson;

        protected EditLessonViewModelBase(string modelRoute, string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {
            _modelRoute = modelRoute;
            _goToClashingLessonCommand = new(GoToClashingLessonAsync);
        }

        protected abstract LessonServiceBase<T> LessonService { get; }

        protected abstract object SelectedDateObject { get; }

        protected override IService<T> Service => LessonService;

        public AsyncRelayCommand GoToClashingLessonCommand => _goToClashingLessonCommand;

        private async Task GoToClashingLessonAsync()
        {
            if (SelectedClashingLesson is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedClashingLesson.Id
                };
                await GoToAsync(parameters, _modelRoute);
            }
        }

        protected override void SetDisplayValues()
        {
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
            SetDisplayDateObject();
        }

        protected abstract void SetDisplayDateObject();

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