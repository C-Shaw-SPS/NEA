using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Models;

namespace OpenOpusDatabase.Tests
{
    public class WorkGetterTests
    {
        const string FILE_PATH = "workResponse.json";

        [Fact]
        public void TestGetFromFile()
        {
            List<Work> actualWorks = WorkGetter.GetFromFile(FILE_PATH);

            Assert.Equal(Expected.Works.Count, actualWorks.Count);

            for (int i = 0; i < Expected.Works.Count; i++)
            {
                Assert.Equal(Expected.Works[i], actualWorks[i]);
            }
        }
    }
}