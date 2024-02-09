using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditPupilCaregiverViewModel : SearchableEditViewModel<PupilCaregiver, CaregiverData, EditCaregiverViewModel>, IQueryAttributable, IViewModel
    {        
        private const string _ROUTE = nameof(EditPupilCaregiverViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit caregiver";
        private const string _NEW_PAGE_TITLE = "New caregiver";
        private const string _NO_CAREGIVER_SELECTED_ERROR = "No caregiver selected";
        private const string _SEARCH_ORDERING = nameof(CaregiverData.Name);

        private readonly PupilCaregiverService _pupilCaregiverService;
        private readonly CaregiverService _caregiverService;
        private int? _pupilId;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        public EditPupilCaregiverViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE, _NO_CAREGIVER_SELECTED_ERROR)
        {
            _pupilCaregiverService = new(_database);
            _caregiverService = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<PupilCaregiver> Service => _pupilCaregiverService;

        protected override ISearchService<CaregiverData> SearchService => _caregiverService;

        protected override string SearchOrdering => _SEARCH_ORDERING;

        protected override void SetDisplayValues()
        {
            Description = _value.Description;
            Name = _value.Name;
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(IPupilDataViewModel.PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                _pupilId = pupilId;
            }
            base.ApplyQueryAttributes(query);
        }

        protected override void UpdateSelectedItemText(CaregiverData value)
        {
            SelectedItemText = value.Name;
        }

        protected override void SetSearchValuesToSave(CaregiverData selectedItem)
        {
            _value.CaregiverId = selectedItem.Id;
        }

        protected override bool TrySetNonSearchValuesToSave()
        {
            _value.Description = Description;
            bool canSave = TrySetPupilIdToSave();
            return canSave;
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
    }
}