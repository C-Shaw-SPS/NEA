using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels
{
    public class ComposerViewModel : ViewModelBase
    {
        public const string ROUTE = nameof(ComposerViewModel);

        private ComposerData _composer;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}