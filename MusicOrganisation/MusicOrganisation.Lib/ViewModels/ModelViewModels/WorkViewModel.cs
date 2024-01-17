using MusicOrganisation.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels.ModelViewModels
{
    public class WorkViewModel : ModelViewModelBase<WorkData>, IViewModel
    {
        private const string _ROUTE = nameof(WorkViewModel);

        public WorkViewModel(string editViewModelRoute) : base(editViewModelRoute)
        {
        }

        public static string Route => _ROUTE;

        protected override void SetDisplayValues()
        {
            throw new NotImplementedException();
        }
    }
}
