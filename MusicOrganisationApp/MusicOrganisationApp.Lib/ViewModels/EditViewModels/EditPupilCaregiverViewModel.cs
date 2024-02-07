﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using System.Collections.ObjectModel;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditPupilCaregiverViewModel : EditViewModelBase<PupilCaregiver>, IQueryAttributable
    {
        public const string ROUTE = nameof(EditPupilCaregiverViewModel);
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private const string _EDIT_PAGE_TITLE = "Edit caregiver";
        private const string _NEW_PAGE_TITLE = "New caregiver";
        private const string _NO_CAREGIVER_SELECTED_ERROR = "No caregiver selected";


        private readonly PupilCaregiverService _pupilCaregiverService;
        private readonly CaregiverService _caregiverService;
        private readonly AsyncRelayCommand _searchCaregiversCommand;
        private readonly AsyncRelayCommand _addNewCaregiverCommand;
        private int? _pupilId;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _caregiverSearchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<CaregiverData> _caregivers = [];

        [ObservableProperty]
        private CaregiverData? _selectedCaregiver;

        [ObservableProperty]
        private string _caregiverError = string.Empty;

        public EditPupilCaregiverViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _pupilCaregiverService = new(_database);
            _caregiverService = new(_database);
            _searchCaregiversCommand = new(SearchCaregiversAsync);
            _addNewCaregiverCommand = new(AddNewCaregiverAsync);
        }

        public AsyncRelayCommand AddNewCaregiverCommand => _addNewCaregiverCommand;

        public AsyncRelayCommand SearchCaregiversCommand => _searchCaregiversCommand;

        protected override IService<PupilCaregiver> Service => _pupilCaregiverService;

        private async Task SearchCaregiversAsync()
        {
            IEnumerable<CaregiverData> searchResult = await _caregiverService.SearchAsync(CaregiverSearchText, nameof(CaregiverData.Name));
            Caregivers.Clear();
            foreach (CaregiverData caregiver in searchResult)
            {
                Caregivers.Add(caregiver);
            }
        }

        private async Task AddNewCaregiverAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [IS_NEW_PARAMETER] = true
            };
            await GoToAsync(parameters, EditCaregiverViewModel.ROUTE);
        }

        protected override void SetDisplayValues()
        {
            Description = _value.Description;
            Name = _value.Name;
        }

        partial void OnSelectedCaregiverChanged(CaregiverData? value)
        {
            if (value is not null)
            {
                Name = value.Name;
            }
        }

        protected override Task<bool> TrySetValuesToSave()
        {
            _value.Description = Description;

            bool canSave = true;
            canSave &= TrySetPupilIdToSave();
            canSave &= TrySetCaregiverToSave();
            return Task.FromResult(canSave);
        }

        private bool TrySetPupilIdToSave()
        {
            if (_pupilId is int pupilId)
            {
                _value.PupilId = pupilId;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TrySetCaregiverToSave()
        {
            if (SelectedCaregiver is not null)
            {
                _value.CaregiverId = SelectedCaregiver.Id;
                _value.Name = SelectedCaregiver.Name;
                CaregiverError = string.Empty;
                return true;
            }
            else if (_isNew)
            {
                CaregiverError = _NO_CAREGIVER_SELECTED_ERROR;
                return false;
            }
            else
            {
                CaregiverError = string.Empty;
                return true;
            }
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                _pupilId = pupilId;
            }
            base.ApplyQueryAttributes(query);
        }
    }
}