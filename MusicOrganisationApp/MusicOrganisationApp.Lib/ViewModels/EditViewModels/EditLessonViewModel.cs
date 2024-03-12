using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditLessonViewModel : EditLessonViewModelBase<Lesson, LessonData, LessonViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditLessonViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit lesson";
        private const string _NEW_PAGE_TITLE = "New lesson";
        private const string _NO_PUPIL_ERROR = "Must select pupil";

        private readonly LessonService _lessonService;
        private readonly PupilService _pupilService;
        private readonly AsyncRelayCommand _searchPupilsCommand;

        [ObservableProperty]
        private DateTime _date = DateTime.Today;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private string _pupilName = string.Empty;

        [ObservableProperty]
        private string _pupilSearchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Pupil> _pupils = [];

        [ObservableProperty]
        private Pupil? _selectedPupil;

        [ObservableProperty]
        private string _pupilError = string.Empty;

        public EditLessonViewModel() : base(LessonViewModel.Route, _EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _lessonService = new(_database);
            _pupilService = new(_database);
            _searchPupilsCommand = new(SearchPupilsAsync);
        }

        public static string Route => _ROUTE;

        protected override LessonServiceBase<Lesson, LessonData> LessonService => _lessonService;

        protected override object SelectedDateObject => Date;

        public AsyncRelayCommand SearchPupilsCommand => _searchPupilsCommand;

        private async Task SearchPupilsAsync()
        {
            IEnumerable<Pupil> pupils = await _pupilService.SearchAsync(PupilSearchText, nameof(Pupil.Name));
            ResetCollection(Pupils, pupils);
        }

        protected override bool TrySetNonTimeValuesToSave()
        {
            _value.Date = Date;
            _value.Notes = Notes;
            bool canSave = TrySavePupil();
            return canSave;
        }

        private bool TrySavePupil()
        {
            if (SelectedPupil is not null)
            {
                _value.PupilId = SelectedPupil.Id;
                PupilError = string.Empty;
                return true;
            }
            else
            {
                PupilError = _NO_PUPIL_ERROR;
                return false;
            }
        }

        protected override void SetDisplayNonTimeValues()
        {
            Date = _value.Date;
            Notes = _value.Notes;
            PupilName = _value.PupilName;
        }

        partial void OnSelectedPupilChanged(Pupil? value)
        {
            if (value is not null)
            {
                PupilName = value.Name;
            }
        }

        public override async Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
        {
            if (query.TryGetValue(IPupilDataViewModel.PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                (bool succeeded, Pupil pupil) = await _pupilService.TryGetAsync(pupilId);
                if (succeeded)
                {
                    SelectedPupil = pupil;
                }
                else
                {
                    await GoBackAsync();
                    return;
                }
            }
            await base.ApplyQueryAttributesAsync(query);
        }
    }
}