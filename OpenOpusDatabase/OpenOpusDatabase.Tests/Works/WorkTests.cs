using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests.Works
{
    public class WorkTests
    {
        const string FILE_PATH = "Works/work.json";

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