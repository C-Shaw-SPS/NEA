﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class PupilAvailabilityViewModel : ViewModelBase, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(PupilAvailabilityViewModel);

        private readonly PupilAvailabilityService _service;
        private readonly AsyncRelayCommand _addNewCommand;
        private readonly AsyncRelayCommand _removeCommand;

        [ObservableProperty]
        private ObservableCollection<LessonSlotData> _lessonSlots = [];

        [ObservableProperty]
        private LessonSlotData? _selectedLessonSlot;

        [ObservableProperty]
        private bool _canRemove = false;

        public PupilAvailabilityViewModel()
        {
            _service = new(_database);
            _addNewCommand = new(AddNewAsync);
            _removeCommand = new(RemoveAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand AddNewCommand => _addNewCommand;

        public AsyncRelayCommand RemoveCommand => _removeCommand;

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }
        public async Task RefreshAsync()
        {
            IEnumerable<LessonSlotData> lessonSlots = await _service.GetPupilAvailabilityAsync();
            ResetCollection(LessonSlots, lessonSlots);
        }

        private async Task AddNewAsync()
        {
            if (PupilId is int pupilId)
            {
                await GoToPupilDataAsync<AddPupilAvailabilityViewModel>(pupilId);
            }
        }

        private async Task RemoveAsync()
        {
            if (SelectedLessonSlot is not null)
            {
                await _service.RemoveAvailabilityAsync(SelectedLessonSlot);
            }
            await RefreshAsync();
        }

        partial void OnSelectedLessonSlotChanged(LessonSlotData? value)
        {
            CanRemove = true;
        }
    }
}