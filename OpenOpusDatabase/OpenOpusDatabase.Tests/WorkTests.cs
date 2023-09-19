using Newtonsoft.Json.Schema;
using OpenOpusDatabase.Lib.APIFetching;
using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Tests
{
    public class WorkTests
    {
        const string FILE_PATH = "work.json";

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