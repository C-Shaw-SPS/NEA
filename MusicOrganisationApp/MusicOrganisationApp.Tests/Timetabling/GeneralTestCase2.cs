using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    public class GeneralTestCase2 : ITimetableTestCase
    {
        private const bool _IS_POSSIBLE = true;

        #region Data

        private static readonly List<LessonSlotData> _lessonSlots = new()
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
                EndTime = new TimeSpan(13,00,00),
                FlagIndex = 9
            }
        };

        private static readonly List<Pupil> _pupils = new()
        {
            new Pupil
            {
                Id = 0,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false
            },
            new Pupil
            {
                Id = 1,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = false
            },
            new Pupil
            {
                Id = 2,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = true
            },
            new Pupil
            {
                Id = 3,
                LessonDuration = new TimeSpan(00,45,00),
                NeedsDifferentTimes = true
            },
            new Pupil
            {
                Id = 4,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = true
            },
            new Pupil
            {
                Id = 5,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false
            },
            new Pupil
            {
                Id = 6,
                LessonDuration = new TimeSpan(00,45,00),
                NeedsDifferentTimes = true
            },
            new Pupil
            {
                Id = 7,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false
            }
        };

        private static readonly List<LessonData> _prevLessons = new()
        {
            new LessonData
            {
                Id = 0,
                PupilId = _pupils[0].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new LessonData
            {
                Id = 1,
                PupilId = _pupils[1].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new LessonData
            {
                Id = 2,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new LessonData
            {
                Id = 3,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new LessonData
            {
                Id = 4,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new LessonData
            {
                Id = 5,
                PupilId = _pupils[5].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new LessonData
            {
                Id = 6,
                PupilId = _pupils[6].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new LessonData
            {
                Id = 7,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[6].Id
            }
        };

        private static readonly Dictionary<int, int> _expectedTimetable = new()
        {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 7 },
            { 4, 4 },
            { 5, 5 },
            { 6, 3 },
            { 7, 6 }
        };

        private static readonly List<PupilLessonSlotData> _pupilLessonSlots = new()
        {
            new PupilLessonSlotData
            {
                Id = 0,
                PupilId = _pupils[0].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new PupilLessonSlotData
            {
                Id = 1,
                PupilId = _pupils[1].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilLessonSlotData
            {
                Id = 2,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new PupilLessonSlotData
            {
                Id = 3,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new PupilLessonSlotData
            {
                Id = 4,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new PupilLessonSlotData
            {
                Id = 5,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilLessonSlotData
            {
                Id = 6,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new PupilLessonSlotData
            {
                Id = 7,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilLessonSlotData
            {
                Id = 8,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilLessonSlotData
            {
                Id = 9,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new PupilLessonSlotData
            {
                Id = 10,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new PupilLessonSlotData
            {
                Id = 11,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new PupilLessonSlotData
            {
                Id = 12,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilLessonSlotData
            {
                Id = 13,
                PupilId = _pupils[5].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilLessonSlotData
            {
                Id = 14,
                PupilId = _pupils[6].Id,
                LessonSlotId =_lessonSlots[4].Id
            },
            new PupilLessonSlotData
            {
                Id = 15,
                PupilId = _pupils[6].Id,
                LessonSlotId =_lessonSlots[5].Id
            },
            new PupilLessonSlotData
            {
                Id = 16,
                PupilId = _pupils[6].Id,
                LessonSlotId =_lessonSlots[6].Id
            },
            new PupilLessonSlotData
            {
                Id = 17,
                PupilId = _pupils[6].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new PupilLessonSlotData
            {
                Id = 18,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new PupilLessonSlotData
            {
                Id = 19,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilLessonSlotData
            {
                Id = 20,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new PupilLessonSlotData
            {
                Id = 21,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new PupilLessonSlotData
            {
                Id = 22,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[6].Id
            }
        };

        #endregion

        public static bool IsPossible => _IS_POSSIBLE;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<LessonSlotData> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static Dictionary<int, int>? ExpectedTimetable => _expectedTimetable;

        public static IEnumerable<PupilLessonSlotData> PupilLessonSlots => _pupilLessonSlots;
    }
}