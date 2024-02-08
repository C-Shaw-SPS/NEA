using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilCaregiversViewModel : SearchableCollectionViewModel<PupilCaregiver, PupilCaregiverViewModel, EditPupilCaregiverViewModel>, IQueryAttributable, IViewModel
    {
        public const string _ROUTE = nameof(AllPupilCaregiversViewModel);

        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(PupilCaregiver.Name)
        };

        private readonly PupilCaregiverService _service;

        public AllPupilCaregiversViewModel() : base(_orderings)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<PupilCaregiver> SearchService => _service;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                _service.PupilId = pupilId;
            }
        }

        protected override void AddAddNewParameters(Dictionary<string, object> parameters)
        {
            if (_service.PupilId is not null)
            {
                parameters[PUPIL_ID_PARAMETER] = _service.PupilId;
            }
        }
    }
}