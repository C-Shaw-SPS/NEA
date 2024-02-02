using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class RepertoireViewModel : ModelViewModelBase<Repertoire>, IQueryAttributable
    {
        private const string _ROUTE = nameof(RepertoireViewModel);
        private const string _DATE_FORMAT = "dd/mm/yyyy";

        private readonly RepertoireService _service;
        private readonly AsyncRelayCommand _goToWorkCommand;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private string _genre = string.Empty;

        [ObservableProperty]
        private string _composerName = string.Empty;

        [ObservableProperty]
        private string _dateStarted = string.Empty;

        [ObservableProperty]
        private string _syllabus = string.Empty;

        [ObservableProperty]
        private bool _isFinishedLearning;

        [ObservableProperty]
        private string _notes = string.Empty;

        public RepertoireViewModel() : base(EditRepertoireViewModel.Route)
        {
            _service = new(_database);
            _goToWorkCommand = new(GoToWorkAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand GoToWorkCommand => _goToWorkCommand;

        protected override IService<Repertoire> Service => _service;

        private async Task GoToWorkAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [ID_PARAMETER] = _value.WorkId
            };
            await GoToAsync(parameters, WorkViewModel.Route);
        }

        protected override void SetDisplayValues()
        {
            Title = _value.Title;
            Subtitle = _value.Subtitle;
            Genre = _value.Genre;
            ComposerName = _value.ComposerName;
            DateStarted = GetDisplayDateTime(_value.DateStarted);
            Syllabus = _value.Syllabus;
            IsFinishedLearning = _value.IsFinishedLearning;
            Notes = _value.Notes;
        }

        private static string GetDisplayDateTime(DateTime? possiblyNullDateTime)
        {
            if (possiblyNullDateTime is DateTime dateTime)
            {
                return dateTime.ToString(_DATE_FORMAT);
            }
            else
            {
                return string.Empty;
            }
        }

        protected override void AddEditRouteParameters(Dictionary<string, object> parameters)
        {
            parameters[EditRepertoireViewModel.PUPIL_ID_PARAMETER] = _value.PupilId;
        }
    }
}