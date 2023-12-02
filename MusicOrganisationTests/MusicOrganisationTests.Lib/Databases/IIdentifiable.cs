using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Databases
{
    public interface IIdentifiable
    {
        public abstract int Id { get; }
    }
}
