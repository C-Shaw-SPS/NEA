using MusicOrganisationTests.Lib.APIFetching;
using MusicOrganisationTests.Lib.Models;

namespace MusicOrganisationTests.Tests.Models
{
    public class WorkGetterTests
    {
        const string FILE_PATH = "Models/workResponse.json";

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
