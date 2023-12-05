using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.App
{
    public static class DefaultValues
    {
        public readonly static List<Pupil> Pupils = new()
        {
            new Pupil
            {
                Id = 0,
                Name = "Pupil 0",
                NeedsDifferentTimes = true,
                LessonDuration = TimeSpan.FromMinutes(30),
                MondayLessonSlots = 0b1001,
                WednesdayLessonSlots = 0b1101,
            },
            new Pupil
            {
                Id = 1,
                Name = "Pupil 1",
                NeedsDifferentTimes = false,
                LessonDuration = TimeSpan.FromMinutes(60),
                MondayLessonSlots = 0b1010,
                WednesdayLessonSlots = 0b1001
            }
        };

        public readonly static List<LessonSlotData> LessonSlotData = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 0,
                StartTime = new TimeSpan(09,00,00),
                EndTime = new TimeSpan(10,00,00),
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 2,
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(10,30,00)
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 3,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(12,00,00)
            },
            new LessonSlotData
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Wednesday,
                FlagIndex = 0,
                StartTime = new TimeSpan(09,30,00),
                EndTime = new TimeSpan(10,00,00)
            },
            new LessonSlotData
            {
                Id = 4,
                DayOfWeek = DayOfWeek.Wednesday,
                FlagIndex = 1,
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(10,30,00)
            },
            new LessonSlotData
            {
                Id = 5,
                DayOfWeek = DayOfWeek.Wednesday,
                FlagIndex = 3,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(12,00,00)
            }
        };

    }
}
