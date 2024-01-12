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
            await service.DropTableIfExistsAsync<ComposerData>();
            ComposerData expectedComposer = Expected.ComposerData[0];
            await service.InsertComposerAsync(
                expectedComposer.Name,
                expectedComposer.BirthYear,
                expectedComposer.DeathYear,
                expectedComposer.Era
                );

            IEnumerable<ComposerData> actualComposers = await service.GetAllAsync<ComposerData>();
            Assert.Single(actualComposers);
            ComposerData actualComposer = actualComposers.First();
            Assert.Equal(expectedComposer, actualComposer);
        }
    }
}