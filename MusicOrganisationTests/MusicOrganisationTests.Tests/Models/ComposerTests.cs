using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Tests.Models
{
    public class ComposerTests
    {
        const string COMPOSER_PATH = "Models/composer.json";
        const string RESPONSE_PATH = "Models/composerResponse.json";

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
            Composer? actualComposer = JsonGetter.GetFromFile<Composer>(COMPOSER_PATH);
            Assert.NotNull(actualComposer);
            Assert.Equal(expectedComposer, actualComposer);
        }

        [Fact]
        public async Task TestComposerSql()
        {
            Database<Composer> database = new(nameof(TestComposerSql));
            await database.ClearAsync();
            await database.InsertAllAsync(Expected.Composers);
            IEnumerable<Composer> actualComposers = await database.GetAllAsync();

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());
            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }

        [Fact]
        public void TestComposerGetter()
        {
            IEnumerable<Composer> actualComposers = ComposerGetter.GetFromFile(RESPONSE_PATH);

            Assert.Equal(Expected.Composers.Count, actualComposers.Count());

            foreach (Composer expectedComposer in Expected.Composers)
            {
                Assert.Contains(expectedComposer, actualComposers);
            }
        }
    }
}