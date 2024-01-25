using MusicOrganisationApp.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllWorksViewModel : ViewModelBase
    {
        private readonly WorkService _service;

        public AllWorksViewModel()
        {
            _service = new(_database);
        }
    }
}