using MusicOrganisationTests.Lib.Json;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Json
{
    public class ComposerTests
    {
        const string COMPOSER_PATH = "Json/composer.json";
        const string RESPONSE_PATH = "Json/composerResponse.json";

        readonly ComposerData expectedComposer = new()
        {
            Id = 87,
            Name = "Bach",
            CompleteName = "Johann Sebastian Bach",
            BirthDate = DateTime.Parse("1685-01-01"),
            DeathDate = DateTime.Parse("1750-01-01"),
            Era = "Baroque",
            PortraitLink = "https://assets.openopus.org/portraits/12091447-1568084857.jpg"
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

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());

            foreach (ComposerData expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}