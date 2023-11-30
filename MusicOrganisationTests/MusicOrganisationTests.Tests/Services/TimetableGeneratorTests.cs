using MusicOrganisationTests.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Services
{
    public class TimetableGeneratorTests
    {
        List<PupilData> pupils = new()
        {
            new PupilData
            {
                Id = 0,
                Name = "Pupil 0",
                NeedsDifferentTimes = true,

            }
        };

        [Fact]
        public void TestTimetableGenerator()
        {
            
        }
    }
}
