using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilsViewModel : CollectionViewModelBase<Pupil>
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

        protected override IService<Pupil> Service => _service;
    }
}
