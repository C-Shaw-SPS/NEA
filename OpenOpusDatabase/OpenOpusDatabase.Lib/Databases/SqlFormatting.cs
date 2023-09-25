using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Databases
{
    internal static class SqlFormatting
    {
        public static string CommaJoin(this List<string> list)
        {
            return "(" + String.Join(", ", list) + ")";
        }
    }
}
