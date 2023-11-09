using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Services
{
    public class ComposerServiceTests
    {

        [Fact]
        public async Task TestInsertComposer()
        {
            ComposerService service = new(nameof(TestInsertComposer));
            await service.ClearTableAsync<ComposerData>();
            ComposerData expectedComposer = Expected.Composers[0];
            await service.InsertComposerAsync(
                expectedComposer.Name,
                expectedComposer.CompleteName,
                expectedComposer.BirthDate,
                expectedComposer.DeathDate,
                expectedComposer.Era,
                expectedComposer.PortraitLink
                );

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            ComposerData actualComposer = actualComposers.First();
            Assert.Equal(expectedComposer, actualComposer);
        }
    }
}