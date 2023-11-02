﻿using MusicOrganisationTests.Lib.Models;
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
        public async Task TestInsertComposer()
        {
            ComposerService service = new(nameof(TestInsertComposer));
            await service.ClearData();
            Composer expectedComposer = Expected.Composers[0];
            await service.InsertAsync(
                expectedComposer.Name,
                expectedComposer.CompleteName,
                expectedComposer.BirthDate,
                expectedComposer.DeathDate,
                expectedComposer.Era,
                expectedComposer.PortraitLink
                );

            IEnumerable<Composer> actualComposers = await service.GetAllAsync();
            Assert.Single(actualComposers);
            Composer actualComposer = actualComposers.First();
            Assert.Equal(expectedComposer, actualComposer);
        }
    }
}