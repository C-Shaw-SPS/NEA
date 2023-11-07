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
            await service.ClearDataAsync<Composer>();
            Composer expectedComposer = Expected.Composers[0];
            await service.InsertComposerAsync(
                expectedComposer.Name,
                expectedComposer.CompleteName,
                expectedComposer.BirthDate,
                expectedComposer.DeathDate,
                expectedComposer.Era,
                expectedComposer.PortraitLink
                );

            IEnumerable<Composer> actualComposers = await service.GetAllAsync<Composer>();
            Assert.Single(actualComposers);
            Composer actualComposer = actualComposers.First();
            Assert.Equal(expectedComposer, actualComposer);
        }
    }
}