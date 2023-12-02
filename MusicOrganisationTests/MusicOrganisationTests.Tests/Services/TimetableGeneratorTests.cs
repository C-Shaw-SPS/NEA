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
        List<LessonSlotData> _lessonSlots = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 0,
                StartTime = new TimeSpan(09,00,00),
                EndTime = new TimeSpan(09,30,00)
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 1,
                StartTime = new TimeSpan(09,30,00),
                EndTime = new TimeSpan(10,00,00)
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 3,
                StartTime = new TimeSpan(10,15,00),
                EndTime = new TimeSpan(11,00,00)
            },
            new LessonSlotData
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 4,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(12,00,00)
            }
        };

        List<PupilData> pupils = new()
        {
            new PupilData
            {
                Id = 0,
                Name = "Pupil 0",
                NeedsDifferentTimes = true,
                LessonDuration = TimeSpan.FromMinutes(30),
                MondayLessonSlots = 0b1001,
            }
        };

        [Fact]
        public void TestTimetableGenerator()
        {
            
        }
    }
}
