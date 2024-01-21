using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Tests.Databases;

namespace MusicOrganisationApp.Tests.Json
{
    public class WorkTests
    {
        const string WORK_PATH = "Json/work.json";
        const string RESPONSE_PATH = "Json/workResponse.json";

        [Fact]
        public void TestJsonDeserialiseWork()
        {
            WorkData? actualWork = JsonGetter.GetFromFile<WorkData>(WORK_PATH);
            Assert.NotNull(actualWork);
            Assert.Equal(ExpectedJson.Work, actualWork);
        }

        [Fact]
        public void TestWorkResponse()
        {
            IEnumerable<WorkData> actualWorks = WorkGetter.GetFromFile(RESPONSE_PATH);

            Assert.Equal(ExpectedJson.ResponseWorks.Count, actualWorks.Count());

            foreach (WorkData expectedWork in ExpectedJson.ResponseWorks)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}