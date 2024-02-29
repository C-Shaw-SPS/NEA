using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditCaregiverViewModel : EditContactablePersonViewModel<CaregiverData>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(EditCaregiverViewModel);
        private const string _EDIT_PAGE_TITLE = "Edit caregiver";
        private const string _NEW_PAGE_TITLE = "New caregiver";

        private readonly CaregiverService _service;

        public EditCaregiverViewModel() : base(_EDIT_PAGE_TITLE, _NEW_PAGE_TITLE)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<CaregiverData> Service => _service;

        protected override void SetNonContactInfoDisplayValues() { }

        protected override bool TrySetNonContactInfoToSave() => true;
    }
}