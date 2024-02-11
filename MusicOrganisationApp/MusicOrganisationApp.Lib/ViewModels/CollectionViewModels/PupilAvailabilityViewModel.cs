﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class PupilAvailabilityViewModel : CollectionViewModelBase<LessonSlotData>, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(PupilAvailabilityViewModel);

        private readonly PupilAvailabilityService _service;
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
            _removeCommand = new(RemoveAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand RemoveCommand => _removeCommand;

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        protected override async Task AddNewAsync()
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

        protected override async Task<IEnumerable<LessonSlotData>> GetAllAsync()
        {
            IEnumerable<LessonSlotData> lessonSlots = await _service.GetUnusedLessonSlotsAsync();
            return lessonSlots;
        }
    }
}