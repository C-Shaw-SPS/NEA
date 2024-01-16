using MusicOrganisation.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    internal class AllWorksViewModel : CollectionViewModelBase<WorkData>
    {
        public AllWorksViewModel(Dictionary<string, string> orderings) : base(orderings)
        {
        }

        protected override Task AddNewAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task SearchAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task SelectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
