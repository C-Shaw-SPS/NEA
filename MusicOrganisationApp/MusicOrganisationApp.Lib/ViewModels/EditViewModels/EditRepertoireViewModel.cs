using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditRepertoireViewModel : EditViewModelBase<Repertoire>
    {
        private const string _ROUTE = nameof(EditRepertoireViewModel);

        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private readonly RepertoireService _repertoireService;
        private readonly WorkService _workService;

        private readonly AsyncRelayCommand _searchWorksCommand;
        private readonly AsyncRelayCommand _addNewWorkCommand;

        [ObservableProperty]
        private DateTime _dateStarted;

        [ObservableProperty]
        private bool _hasDateStarted = false;

        [ObservableProperty]
        private string _syllabus = string.Empty;

        [ObservableProperty]
        private bool _isFinishedLearning;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private string _workTitle = string.Empty;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Work> _works = [];

        [ObservableProperty]
        private Work? _selectedWork;

        public EditRepertoireViewModel(string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {
            _repertoireService = new(_database);
            _workService = new(_database);
            _searchWorksCommand = new(SearchWorksAsync);
            _addNewWorkCommand = new(AddNewWorkAsync);
        }

        public static string Route => _ROUTE;

        protected override IService<Repertoire> Service => _repertoireService;

        private async Task SearchWorksAsync()
        {
            IEnumerable<Work> works = await _workService.SearchAsync(SearchText, nameof(Work.Title));
            Works.Clear();
            foreach (Work work in works)
            {
                Works.Add(work);
            }
        }

        private async Task AddNewWorkAsync()
        {
            throw new NotImplementedException();
        }
        protected override void SetDisplayValues()
        {
            SetDateStarted();
            Syllabus = _value.Syllabus;
            IsFinishedLearning = _value.IsFinishedLearning;
            Notes = _value.Notes;
            WorkTitle = _value.Title;
        }

        private void SetDateStarted()
        {
            if (_value.DateStarted is DateTime dateStarted)
            {
                DateStarted = dateStarted;
                HasDateStarted = true;
            }
            else
            {
                DateStarted = DateTime.Today;
                HasDateStarted = false;
            }
        }

        protected override bool TrySetValuesToSave()
        {
            throw new NotImplementedException();
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int id)
            {
                _repertoireService.PupilId = id;
            }
            base.ApplyQueryAttributes(query);
        }
    }
}