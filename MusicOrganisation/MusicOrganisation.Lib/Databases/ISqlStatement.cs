using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.Databases
{
    public interface ISqlStatement
    {
        public string GetSql();
    }
}