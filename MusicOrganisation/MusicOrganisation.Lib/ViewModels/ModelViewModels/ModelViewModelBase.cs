using MusicOrganisation.Lib.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels.ModelViewModels
{
    public class ModelViewModelBase<T> : ViewModelBase
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);
    }
}