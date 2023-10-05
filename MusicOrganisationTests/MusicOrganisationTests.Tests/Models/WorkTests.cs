using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Tests.Models
{
    public class WorkTests
    {
        const string FILE_PATH = "Models/work.json";

        readonly Work expectedWork = new()
        {
            Id = 20086,
            ComposerId = 176,
            Title = "3 Movements",
            Subtitle = "",
            Genre = "Orchestral",
        };

        [Fact]
        public void TestJsonDeserialiseWork()
        {
            Work? actualWork = JsonGetter.GetFromFile<Work>(FILE_PATH);
            Assert.NotNull(actualWork);
            Assert.Equal(expectedWork, actualWork);
        }
    }
}