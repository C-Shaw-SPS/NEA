﻿using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    internal class AllDifferentTimesTestCase : ITimetableTestCase
    {
        private const bool _IS_POSSIBLE = true;

        #region Data

        private static readonly List<LessonSlotData> _lessonSlots = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(09, 00, 00),
                EndTime = new TimeSpan(10, 00, 00)
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(11, 00, 00)
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(11, 00, 00),
                EndTime = new TimeSpan(12, 00, 00)
            },
            new LessonSlotData
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(12, 00, 00),
                EndTime = new TimeSpan(13, 00, 00)
            }
        };

        private static readonly List<Pupil> _pupils = new()
        {
            new Pupil
            {
                Id = 0,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 1,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 2,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 3,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            }
        };

        private static readonly List<LessonData> _prevLessons = new()
        {
            new LessonData
            {
                PupilId = 0,
                LessonSlotId = 1
            },
            new LessonData
            {
                PupilId = 1,
                LessonSlotId = 2
            },
            new LessonData
            {
                PupilId = 2,
                LessonSlotId = 3
            },
            new LessonData
            {
                PupilId = 3,
                LessonSlotId = 0
            },
        };

        private static readonly Dictionary<int, int> _expectedTimetable = new()
        {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 3 }
        };

        private static readonly List<PupilLessonSlotData> _pupilLessonSlots = GetPupilLessonSlots();

        private static List<PupilLessonSlotData> GetPupilLessonSlots()
        {
            List<PupilLessonSlotData> pupilLessonSlots = [];
            int id = 0;
            for (int i = 0; i < _pupils.Count; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    PupilLessonSlotData pupilLessonSlot = new()
                    {
                        Id = id,
                        PupilId = _pupils[i].Id,
                        LessonSlotId = _lessonSlots[(i + j) % _lessonSlots.Count].Id
                    };
                    pupilLessonSlots.Add(pupilLessonSlot);
                    ++id;
                }
            }
            return pupilLessonSlots;
        }


        #endregion

        public static bool IsPossible => _IS_POSSIBLE;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<LessonSlotData> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static Dictionary<int, int>? ExpectedTimetable => _expectedTimetable;

        public static IEnumerable<PupilLessonSlotData> PupilLessonSlots => _pupilLessonSlots;
    }
}
