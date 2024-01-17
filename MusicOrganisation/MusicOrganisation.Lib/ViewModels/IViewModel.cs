using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels
{
    public interface IViewModel
    {
        public abstract static string Route { get; }
    }
}