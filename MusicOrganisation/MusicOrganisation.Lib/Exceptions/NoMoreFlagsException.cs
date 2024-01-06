using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.Exceptions
{
    public class NoMoreFlagsException : Exception
    {
        public NoMoreFlagsException(DayOfWeek dayOfWeek) : base($"No more flags avaliable on {dayOfWeek}") { }
    }
}