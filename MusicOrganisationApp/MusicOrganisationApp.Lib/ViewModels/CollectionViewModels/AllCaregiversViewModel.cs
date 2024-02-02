using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllCaregiversViewModel : CollectionViewModelBase<CaregiverData>
    {
        private static Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(CaregiverData.Name)
        };

        private readonly CaregiverService _service;

        public AllCaregiversViewModel() : base(CaregiverViewModel.Route, EditCaregiverViewModel.Route, _orderings)
        {
            _service = new(_database);
        }

        protected override IService<CaregiverData> Service => _service;
    }
}
