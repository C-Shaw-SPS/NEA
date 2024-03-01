using MusicOrganisationApp.Lib.Json;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Json
{
    public class WorkTests
    {
        private const string WORK_PATH = "Json/work.json";
        private const string RESPONSE_PATH = "Json/workResponse.json";

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
            CollectionAssert.EqualContents(ExpectedJson.ResponseWorks, actualWorks);
        }
    }
}