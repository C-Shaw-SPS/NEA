using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.Exceptions
{
    public class FlagsException : Exception
    {
        public FlagsException() : base() { }

        public FlagsException(string message) : base(message) { }
    }
}
