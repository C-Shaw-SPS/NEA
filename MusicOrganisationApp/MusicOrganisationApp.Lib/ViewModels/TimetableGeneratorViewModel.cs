using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public partial class TimetableGeneratorViewModel : ViewModelBase, IViewModel
    {
        private const string _ROUTE = nameof(TimetableGeneratorViewModel);
        private const string _LESSON_DELETION_WARNING = "Warning: lessons in specified week will be deleted";
        private const string _SUCESSFUL_TIMETABLE_GENERATION = "Timetable generated sucessfuly.";
        private const string _UNSUCESSFUL_TIMETABLE_GENERATION = "Timbetabling conditions could not be met. Edit conditions or create timetable manually";

        private readonly TimetableService _service;
        private readonly AsyncRelayCommand _generateTimetableCommand;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Today;

        [ObservableProperty]
        private string _message = _LESSON_DELETION_WARNING;

        public TimetableGeneratorViewModel()
        {
            _service = new(_database);
            _generateTimetableCommand = new(GenerateTimetableAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand GenerateTimetableCommand => _generateTimetableCommand;

        private async Task GenerateTimetableAsync()
        {
            bool suceeded = await _service.TryGenerateTimetable(SelectedDate);
            if (suceeded)
            {
                Message = _SUCESSFUL_TIMETABLE_GENERATION;
            }
            else
            {
                Message = _UNSUCESSFUL_TIMETABLE_GENERATION;
            }
        }

        partial void OnSelectedDateChanged(DateTime value)
        {
            Message = _LESSON_DELETION_WARNING;
        }
    }
}