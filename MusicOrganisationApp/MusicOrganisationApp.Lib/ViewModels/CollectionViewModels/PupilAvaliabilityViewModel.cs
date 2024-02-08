using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class PupilAvaliabilityViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ROUTE = nameof(PupilAvaliabilityViewModel);
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private readonly PupilAvaliabilityService _service;
        private readonly AsyncRelayCommand _addNewCommand;
        private readonly AsyncRelayCommand _removeCommand;
        private int? _pupilId;

        [ObservableProperty]
        private ObservableCollection<LessonSlotData> _lessonSlots = [];

        [ObservableProperty]
        private LessonSlotData? _selectedLessonSlot;

        [ObservableProperty]
        private bool _canRemove = false;

        public PupilAvaliabilityViewModel()
        {
            _service = new(_database);
            _addNewCommand = new(AddNewAsync);
            _removeCommand = new(RemoveAsync);
        }

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public AsyncRelayCommand RemoveCommand => _removeCommand;

        public int? PupilId
        {
            get => _pupilId;
            set
            {
                _pupilId = value;
                _service.PupilId = value;
            }
        }

        public async Task RefreshAsync()
        {
            IEnumerable<LessonSlotData> lessonSlots = await _service.GetPupilAvaliabilityAsync();
            ResetCollection(LessonSlots, lessonSlots);
        }

        private async Task AddNewAsync()
        {
            if (_pupilId is not null)
            {
                Dictionary<string, object> parameters = new()
                {
                    [AddPupilAvaliabilityViewModel.PUPIL_ID_PARAMETER] = _pupilId
                };
                await GoToAsync(parameters, AddPupilAvaliabilityViewModel.ROUTE);
            }
        }

        private async Task RemoveAsync()
        {
            if (SelectedLessonSlot is not null)
            {
                await _service.RemoveAvaliabilityAsync(SelectedLessonSlot);
            }
        }

        partial void OnSelectedLessonSlotChanged(LessonSlotData? value)
        {
            CanRemove = true;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                PupilId = pupilId;
            }
        }
    }
}
