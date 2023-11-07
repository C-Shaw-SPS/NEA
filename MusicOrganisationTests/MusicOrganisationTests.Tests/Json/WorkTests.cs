using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Tests.Models
{
    public class WorkTests
    {
        const string WORK_PATH = "Json/work.json";
        const string RESPONSE_PATH = "Json/workResponse.json";

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
            Work? actualWork = JsonGetter.GetFromFile<Work>(WORK_PATH);
            Assert.NotNull(actualWork);
            Assert.Equal(expectedWork, actualWork);
        }

        [Fact]
        public void TestWorkResponse()
        {
            IEnumerable<Work> actualWorks = WorkGetter.GetFromFile(RESPONSE_PATH);

            Assert.Equal(Expected.Works.Count, actualWorks.Count());

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}