using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Json
{
    public class ComposerTests
    {
        private const string _COMPOSER_PATH = "Json/composer.json";
        private const string _RESPONSE_PATH = "Json/composerResponse.json";

        [Fact]
        public void TestJsonDeserialiseComposer()
        {
            Composer? actualComposer = JsonGetter.GetFromFile<Composer>(_COMPOSER_PATH);
            Assert.NotNull(actualComposer);
            Assert.Equal(ExpectedJson.Composer, actualComposer);
        }

        [Fact]
        public void TestComposerGetter()
        {
            IEnumerable<Composer> actualComposers = ComposerGetter.GetFromFile(_RESPONSE_PATH);

            CollectionAssert.EqualContents(ExpectedJson.ResponseComposers, actualComposers);
        }
    }
}