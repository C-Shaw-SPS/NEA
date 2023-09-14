using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Tests
{
    public class WorkGetterTests
    {
        const string FILE_PATH = "workResponse.json";

        readonly List<Work> expectedWorks = new()
        {
            new Work
            {
                Id = 1,
                ComposerId = 176,
                Title = "3 Movements",
                Subtitle = "",
                Genre = "Orchestral"
            },
            new Work
            {
                Id = 2,
                ComposerId = 202,
                Title = "3 Mouvements perp\u00e9tuels, FP14",
                Subtitle = "",
                Genre = "Keyboard"
            }
        };

        [Fact]
        public void TestGetFromFile()
        {
            List<Work> actualWorks = WorkGetter.GetFromFile(FILE_PATH);

            Assert.Equal(expectedWorks.Count, actualWorks.Count);

            for (int i = 0; i < expectedWorks.Count; i++)
            {
                Assert.Equal(expectedWorks[i], actualWorks[i]);
            }
        }

    }
}
