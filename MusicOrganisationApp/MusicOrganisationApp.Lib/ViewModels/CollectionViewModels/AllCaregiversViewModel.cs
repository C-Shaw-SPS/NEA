using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllCaregiversViewModel : SearchableCollectionViewModel<CaregiverData, CaregiverViewModel, EditCaregiverViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllCaregiversViewModel);

        private static readonly Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(CaregiverData.Name)
        };

        private readonly CaregiverService _service;

        public AllCaregiversViewModel() : base(_orderings, GetDefaultPath(), false)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override ISearchService<CaregiverData> SearchService => _service;
    }
}