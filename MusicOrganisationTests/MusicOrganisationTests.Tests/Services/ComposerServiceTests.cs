using MusicOrganisationTests.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Services
{
    public class ComposerServiceTests
    {
        [Fact]
        public async Task TestAddComposer()
        {
            ComposerService service = new(nameof(TestAddComposer));
            await service.Add(
                Expected.Composers[0].Name,
                Expected.Composers[0].CompleteName,
                Expected.Composers[0].BirthDate,
                Expected.Composers[0].DeathDate,
                Expected.Composers[0].Era,
                Expected.Composers[0].PortraitLink
                );

        }
    }
}