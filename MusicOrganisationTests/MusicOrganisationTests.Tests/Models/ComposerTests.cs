using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Tests.Models
{
    public class ComposerTests
    {
        const string FILE_PATH = "Models/composer.json";

        readonly Composer expectedComposer = new()
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
            Composer? actualComposer = JsonGetter.GetFromFile<Composer>(FILE_PATH);
            Assert.NotNull(actualComposer);
            Assert.Equal(expectedComposer, actualComposer);
        }
    }
}