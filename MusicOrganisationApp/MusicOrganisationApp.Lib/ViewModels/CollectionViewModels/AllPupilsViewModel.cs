using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilsViewModel : SearchableCollectionViewModel<Pupil, PupilViewModel, EditPupilViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllPupilsViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(Pupil.Name)
        };

        private readonly PupilService _service;

        public AllPupilsViewModel() : base(_orderings, GetDefaultPath(), false)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<Pupil> SearchService => _service;
    }
}