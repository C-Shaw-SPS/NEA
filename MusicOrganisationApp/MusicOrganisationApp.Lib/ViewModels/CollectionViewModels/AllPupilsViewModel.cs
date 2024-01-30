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
        private static Dictionary<string, string> _orderings = new()
        {
            ["Name"] = nameof(Pupil.Name)
        };

        public AllPupilsViewModel() : base(PupilViewModel.Route, EditPupilViewModel.Route, _orderings)
        {
        }

        protected override IService<Pupil> Service => throw new NotImplementedException();
    }
}
