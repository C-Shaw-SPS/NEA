﻿using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Services
{
    public class TimetableGeneratorTests
    {
        static List<LessonSlotData> _lessonSlots = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(09,00,00),
                EndTime = new TimeSpan(10,00,00),
                FlagIndex = 0
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(10,30,00),
                FlagIndex = 1
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(11,45,00),
                FlagIndex = 4
            },
            new LessonSlotData
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(12,00,00),
                EndTime = new TimeSpan(13,00,00),
                FlagIndex = 3
            },
            new LessonSlotData
            {
                Id = 4,
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = new TimeSpan(09,00,00),
                EndTime = new TimeSpan(09,30,00),
                FlagIndex = 2
            },
            new LessonSlotData
            {
                Id = 5,
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = new TimeSpan(10,00,00),
                EndTime = new TimeSpan(11,00,00),
                FlagIndex = 5
            },
            new LessonSlotData
            {
                Id = 6,
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = new TimeSpan(11,00,00),
                EndTime = new TimeSpan(12,00,00),
                FlagIndex = 3
            },
            new LessonSlotData
            {
                Id = 7,
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = new TimeSpan(12,15,00),
                EndTime = new TimeSpan(12,00,00),
                FlagIndex = 9
            }
        };

        static List<PupilData> _pupilData = new()
        {
            new PupilData
            {
                Id = 0,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex)
            },
            new PupilData
            {
                Id = 1,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = false,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[1].FlagIndex)
            },
            new PupilData
            {
                Id = 2,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = true,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[2].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[4].FlagIndex, _lessonSlots[6].FlagIndex)
            },
            new PupilData
            {
                Id = 3,
                LessonDuration = new TimeSpan(00,45,00),
                NeedsDifferentTimes = true,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[1].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[6].FlagIndex, _lessonSlots[7].FlagIndex)
            },
            new PupilData
            {
                Id = 4,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = true,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[3].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[4].FlagIndex, _lessonSlots[5].FlagIndex)
            },
            new PupilData
            {
                Id = 5,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false,
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[5].FlagIndex)
            },
            new PupilData
            {
                Id = 6,
                LessonDuration = new TimeSpan(00,45,00),
                NeedsDifferentTimes = true,
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[4].FlagIndex, _lessonSlots[5].FlagIndex, _lessonSlots[6].FlagIndex, _lessonSlots[7].FlagIndex)
            },
            new PupilData
            {
                Id = 7,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[1].FlagIndex, _lessonSlots[2].FlagIndex, _lessonSlots[3].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[6].FlagIndex)
            }
        };

        static List<LessonData> _previousLessons = new()
        {
            new LessonData
            {
                PupilId = _pupilData[0].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new LessonData
            {
                PupilId = _pupilData[1].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new LessonData
            {
                PupilId = _pupilData[2].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new LessonData
            {
                PupilId = _pupilData[3].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new LessonData
            {
                PupilId = _pupilData[4].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new LessonData
            {
                PupilId = _pupilData[5].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new LessonData
            {
                PupilId = _pupilData[6].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new LessonData
            {
                PupilId = _pupilData[7].Id,
                LessonSlotId = _lessonSlots[6].Id
            }
        };

        [Fact]
        public void TestTimetableGenerator()
        {
            IEnumerable<Pupil> pupils = _pupilData.Select(pupil => new Pupil(pupil));
            TimetableGenerator timetableGenerator = new(pupils, _lessonSlots, _previousLessons);
            Dictionary<int, int> timetable = timetableGenerator.GenerateTimetable();
        }
    }
}
