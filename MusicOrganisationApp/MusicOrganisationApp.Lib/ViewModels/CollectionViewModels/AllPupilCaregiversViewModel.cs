using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilCaregiversViewModel : SearchableCollectionViewModel<PupilCaregiver, PupilCaregiverViewModel, EditPupilCaregiverViewModel>, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(AllPupilCaregiversViewModel);

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

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        protected override ISearchService<PupilCaregiver> SearchService => _service;
    }
}