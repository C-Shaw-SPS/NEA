using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Json
{
    public class ComposerTests
    {
        const string COMPOSER_PATH = "Json/composer.json";
        const string RESPONSE_PATH = "Json/composerResponse.json";

        readonly ComposerData expectedComposer = new()
        {
            Id = 87,
            Name = "Johann Sebastian Bach",
            BirthYear = 1685,
            DeathYear = 1750,
            Era = "Baroque"
        };

        [Fact]
        public void TestJsonDeserialiseComposer()
        {
            ComposerData? actualComposer = JsonGetter.GetFromFile<ComposerData>(COMPOSER_PATH);
            Assert.NotNull(actualComposer);
            Assert.Equal(expectedComposer, actualComposer);
        }

        [Fact]
        public void TestComposerGetter()
        {
            IEnumerable<ComposerData> actualComposers = ComposerGetter.GetFromFile(RESPONSE_PATH);

            Assert.Equal(Expected.ComposerData.Count, actualComposers.Count());

            foreach (ComposerData expectedComposer in Expected.ComposerData)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}