using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Tests.Models
{
    public class ComposerGetterTests
    {
        const string FILE_PATH = "Models/composerResponse.json";

        [Fact]
        public void TestGetFromFile()
        {
            IEnumerable<Composer> actualComposers = ComposerGetter.GetFromFile(FILE_PATH);

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());

            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}