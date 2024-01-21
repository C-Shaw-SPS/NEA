using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib
{
    public interface ITable
    {
        public abstract static string TableName { get; set; }

        public abstract int Id { get; set; }

        public static abstract IEnumerable<string> GetColumnNames();

        public abstract IDictionary<string, string> GetSqlValues();
    }
}