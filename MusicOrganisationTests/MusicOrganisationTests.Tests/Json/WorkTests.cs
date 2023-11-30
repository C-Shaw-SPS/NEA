using MusicOrganisationTests.Lib.Json;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Json
{
    public class WorkTests
    {
        const string WORK_PATH = "Json/work.json";
        const string RESPONSE_PATH = "Json/workResponse.json";

        readonly WorkData expectedWork = new()
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
            WorkData? actualWork = JsonGetter.GetFromFile<WorkData>(WORK_PATH);
            Assert.NotNull(actualWork);
            Assert.Equal(expectedWork, actualWork);
        }

        [Fact]
        public void TestWorkResponse()
        {
            IEnumerable<WorkData> actualWorks = WorkGetter.GetFromFile(RESPONSE_PATH);

            Assert.Equal(Expected.WorkData.Count, actualWorks.Count());

            foreach (WorkData expectedWork in Expected.WorkData)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}