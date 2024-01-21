using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Tests.Databases;

namespace MusicOrganisationApp.Tests.Json
{
    public class ComposerTests
    {
        private const string _COMPOSER_PATH = "Json/composer.json";
        private const string _RESPONSE_PATH = "Json/composerResponse.json";

        [Fact]
        public void TestJsonDeserialiseComposer()
        {
            ComposerData? actualComposer = JsonGetter.GetFromFile<ComposerData>(_COMPOSER_PATH);
            Assert.NotNull(actualComposer);
            Assert.Equal(ExpectedJson.Composer, actualComposer);
        }

        [Fact]
        public void TestComposerGetter()
        {
            IEnumerable<ComposerData> actualComposers = ComposerGetter.GetFromFile(_RESPONSE_PATH);

            Assert.Equal(ExpectedJson.ResponseComposers.Count, actualComposers.Count());

            foreach (ComposerData expectedComposer in ExpectedJson.ResponseComposers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}