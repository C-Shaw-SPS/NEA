using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.Databases
{
    internal class CountProperty
    {
        private int _count;

        public int Count
        {
            get => _count;
            set => _count = value;
        }
    }
}
