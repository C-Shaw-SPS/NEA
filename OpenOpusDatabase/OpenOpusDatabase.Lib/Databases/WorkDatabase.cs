using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Databases
{
    public class WorkDatabase : Database<Work>
    {
        public WorkDatabase(string path) : base(path)
        {
        }

        public override async Task InsertAsync(Work value)
        {
            await InitAsync();
            await Connection.InsertAsync(value);
        }
    }
}