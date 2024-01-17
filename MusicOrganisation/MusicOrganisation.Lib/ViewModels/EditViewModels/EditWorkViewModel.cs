using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels.EditViewModels
{
    public class EditWorkViewModel : EditViewModelBase<WorkData, WorkViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(EditWorkViewModel);

        private const string _NEW_PAGE_TITLE = "New work";
        private const string _EDIT_PAGE_TITLE = "Edit work";

        public EditWorkViewModel() : base(_NEW_PAGE_TITLE, _EDIT_PAGE_TITLE)
        {

        }

        public static string Route => _ROUTE;

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
