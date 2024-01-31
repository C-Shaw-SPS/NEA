using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class RepertoireViewModel : ModelViewModelBase<Repertoire>, IQueryAttributable
    {
        private const string _ROUTE = nameof(RepertoireViewModel);

        private readonly RepertoireService _service;

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
        }

        public static string Route => _ROUTE;

        protected override IService<Repertoire> Service => _service;

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
                return dateTime.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}