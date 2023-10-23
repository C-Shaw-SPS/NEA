using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Models
{
    public class RepertoireTests
    {
        [Fact]
        public async Task TestRepertoireSql()
        {
            Database<Repertoire> database = new(nameof(TestRepertoireSql));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Repertoires);
            IEnumerable<Repertoire> actualRepertoires = await database.GetAllAsync();
            foreach (Repertoire repertoire in Expected.Repertoires)
            {
                Assert.Contains(repertoire, actualRepertoires);
            }
        }
    }
}