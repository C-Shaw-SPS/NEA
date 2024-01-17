using MusicOrganisation.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels.EditViewModels
{
    public class EditWorkViewModel : EditViewModelBase<WorkData>
    {
        public const string ROUTE = nameof(EditWorkViewModel);

        public EditWorkViewModel(string newPageTitle, string editPageTitle, string modelViewModelRoute) : base(newPageTitle, editPageTitle, modelViewModelRoute)
        {
        }

        protected override void SetDisplayValues()
        {
            throw new NotImplementedException();
        }

        protected override bool TrySetValuesToSave()
        {
            throw new NotImplementedException();
        }
    }
}
