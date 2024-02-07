using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilsViewModel : SearchableCollectionViewModel<Pupil>
    {
        public const string ROUTE = nameof(AllPupilsViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(Pupil.Name)
        };

        private readonly PupilService _service;

        public AllPupilsViewModel() : base(PupilViewModel.ROUTE, EditPupilViewModel.ROUTE, _orderings)
        {
            _service = new(_database);
        }

        protected override ISearchService<Pupil> SearchService => _service;
    }
}