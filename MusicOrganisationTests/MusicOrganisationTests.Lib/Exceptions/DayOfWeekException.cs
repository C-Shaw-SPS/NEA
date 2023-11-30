using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Exceptions
{
    public class DayOfWeekException : Exception
    {
        public DayOfWeekException(DayOfWeek dayOfWeek) : base($"No such day of week '{dayOfWeek}'") { }
    }
}
