using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests.Works
{
    public class WorkGetterTests
    {
        const string FILE_PATH = "Works/workResponse.json";

        [Fact]
        public void TestGetFromFile()
        {
            IEnumerable<Work> actualWorks = WorkGetter.GetFromFile(FILE_PATH);

            Assert.Equal(Expected.Works.Count, actualWorks.Count());

            foreach (Work expectedWork in Expected.Works)
            {
                Assert.Contains(expectedWork, actualWorks);
            }
        }
    }
}
