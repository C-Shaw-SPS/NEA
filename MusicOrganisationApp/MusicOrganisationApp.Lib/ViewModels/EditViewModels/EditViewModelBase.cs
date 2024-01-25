using MusicOrganisationApp.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public class EditViewModelBase : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
        public const string IS_NEW_PARAMETER = nameof(IS_NEW_PARAMETER);
    }

    public class EditViewModelBase<T> : EditViewModelBase where T : class, IModel, new()
    {

    }
}