using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.Viewmodels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected string _path;

        public ViewModelBase(string path)
        {
            _path = path;
        }

        public ViewModelBase(string directory, string fileName)
        {
            _path = Path.Combine(directory, fileName);
        }
    }
}