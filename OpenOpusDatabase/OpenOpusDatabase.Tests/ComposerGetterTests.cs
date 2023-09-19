using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests
{
    public class ComposerGetterTests
    {
        const string FILE_PATH = "composerResponse.json";

        [Fact]
        public void TestGetFromFile()
        {
            List<Composer> actualComposers = ComposerGetter.GetFromFile(FILE_PATH);

            Assert.Equal(Expected.Composers.Count, actualComposers.Count);

            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}