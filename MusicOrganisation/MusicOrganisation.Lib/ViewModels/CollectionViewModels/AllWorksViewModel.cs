using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels.CollectionViewModels
{
    internal class AllWorksViewModel : CollectionViewModelBase<WorkData>
    {
        private static readonly Dictionary<string, string> _orderings = new()
        {
            { "Title", nameof(WorkData.Title) },
            { "Composer", nameof(WorkData.Genre) }
        };

        public AllWorksViewModel() : base(_orderings, WorkViewModel.ROUTE, nameof(WorkData.Title))
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
