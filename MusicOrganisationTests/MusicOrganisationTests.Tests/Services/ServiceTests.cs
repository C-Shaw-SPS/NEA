using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Services
{
    public class ServiceTests
    {
        [Fact]
        public async Task TestClearDataAsync()
        {
            Service<Composer> service = new ComposerService(nameof(TestClearDataAsync));
            await service.InsertAllAsync(Expected.Composers);
            await service.ClearData();
            IEnumerable<Composer> actualComposers = await service.GetAllAsync();
            Assert.Empty(actualComposers);
        }
    }
}
