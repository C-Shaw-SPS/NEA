using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public abstract partial class EditLessonViewModelBase<TModel, TTable, TModelViewModel> : EditViewModelBase<TModel>, IQueryAttributable
        where TModel : class, ILesson<TTable>, new()
        where TTable : class, ITable, new()
        where TModelViewModel : IViewModel
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
        private ObservableCollection<TModel> _clashingLessons = [];

        [ObservableProperty]
        private TModel? _selectedClashingLesson;

        public EditLessonViewModelBase(string modelRoute, string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle, GetDefaultPath(), false)
        {
            _modelRoute = modelRoute;
            _goToClashingLessonCommand = new(GoToClashingLessonAsync);
        }

        protected abstract LessonServiceBase<TModel, TTable> LessonService { get; }

        protected abstract object SelectedDateObject { get; }

        protected override IService<TModel> Service => LessonService;

        public AsyncRelayCommand GoToClashingLessonCommand => _goToClashingLessonCommand;

        private async Task GoToClashingLessonAsync()
        {
            if (SelectedClashingLesson is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [ModelViewModelBase.ID_PARAMETER] = SelectedClashingLesson.Id
                };
                await GoToAsync<TModelViewModel>(parameters);
            }
        }

        protected override void SetDisplayValues()
        {
            StartTime = _value.StartTime;
            EndTime = _value.EndTime;
            SetDisplayNonTimeValues();
        }

        protected abstract void SetDisplayNonTimeValues();

        protected override async Task<bool> TrySetValuesToSaveAsync()
        {
            bool canSave = true;
            canSave &= TrySetNonTimeValuesToSave();
            canSave &= await TrySetTimesToSave();
            return canSave;
        }

        protected abstract bool TrySetNonTimeValuesToSave();

        private async Task<bool> TrySetTimesToSave()
        {
            if (EndTime < StartTime)
            {
                TimeError = _END_BEFORE_START_ERROR;
                return false;
            }
            int? id = _isNew ? null : _value.Id;
            IEnumerable<TModel> clashingLessons = await LessonService.GetClashingLessonsAsync(SelectedDateObject, StartTime, EndTime, id);
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