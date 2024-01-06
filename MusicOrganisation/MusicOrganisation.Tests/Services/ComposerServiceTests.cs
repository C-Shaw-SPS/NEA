using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;

namespace MusicOrganisation.Tests.Services
{
    public class ComposerServiceTests
    {

        [Fact]
        public async Task TestInsertComposer()
        {
            ComposerService service = new(nameof(TestInsertComposer));
            await service.ClearTableAsync<ComposerData>();
            ComposerData expectedComposer = Expected.ComposerData[0];
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