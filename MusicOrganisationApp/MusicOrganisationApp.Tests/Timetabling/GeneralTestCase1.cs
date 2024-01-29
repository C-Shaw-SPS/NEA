using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    public class GeneralTestCase1 : ITimetableTestCase
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
            },
            new LessonSlotData
            {
                Id = 4,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(12, 00, 00),
                EndTime = new TimeSpan(13, 00, 00)
            },
            new LessonSlotData
            {
                Id = 5,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(13, 00, 00),
                EndTime = new TimeSpan(14, 00, 00)
            },
            new LessonSlotData
            {
                Id = 6,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(14, 00, 00),
                EndTime = new TimeSpan(15, 00, 00)
            },
            new LessonSlotData
            {
                Id = 7,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(15, 00, 00),
                EndTime = new TimeSpan(16, 00, 00)
            }
        };

        private static readonly List<Pupil> _pupils = new()
        {
            new Pupil
            {
                Id = 0,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 1,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 2,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 3,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 4,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 5,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 6,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            },
            new Pupil
            {
                Id = 7,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00)
            }
        };

        private static readonly List<LessonData> _prevLessons = new()
        {
            new LessonData
            {
                PupilId = 0,
                LessonSlotId = 0
            },
            new LessonData
            {
                PupilId = 1,
                LessonSlotId = 1
            },
            new LessonData
            {
                PupilId = 2,
                LessonSlotId = 2
            },
            new LessonData
            {
                PupilId = 3,
                LessonSlotId = 3
            },
            new LessonData
            {
                PupilId = 4,
                LessonSlotId = 5
            },
            new LessonData
            {
                PupilId = 5,
                LessonSlotId = 6
            },
            new LessonData
            {
                PupilId = 6,
                LessonSlotId = 7
            },
            new LessonData
            {
                PupilId = 7,
                LessonSlotId = 4
            },
        };

        private static readonly List<PupilAvaliability> _pupilLessonSlots = new()
        {
            new PupilAvaliability
            {
                Id = 0,
                PupilId = _pupils[0].Id,
                LessonSlotId = _lessonSlots[0].Id
            },
            new PupilAvaliability
            {
                Id = 1,
                PupilId = _pupils[1].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilAvaliability
            {
                Id = 2,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[1].Id
            },
            new PupilAvaliability
            {
                Id = 3,
                PupilId = _pupils[2].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new PupilAvaliability
            {
                Id = 4,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[2].Id
            },
            new PupilAvaliability
            {
                Id = 5,
                PupilId = _pupils[3].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new PupilAvaliability
            {
                Id = 6,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[3].Id
            },
            new PupilAvaliability
            {
                Id = 7,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new PupilAvaliability
            {
                Id = 8,
                PupilId = _pupils[4].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilAvaliability
            {
                Id = 9,
                PupilId = _pupils[5].Id,
                LessonSlotId = _lessonSlots[4].Id
            },
            new PupilAvaliability
            {
                Id = 10,
                PupilId = _pupils[5].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilAvaliability
            {
                Id = 11,
                PupilId = _pupils[5].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilAvaliability
            {
                Id = 12,
                PupilId = _pupils[6].Id,
                LessonSlotId = _lessonSlots[5].Id
            },
            new PupilAvaliability
            {
                Id = 13,
                PupilId = _pupils[6].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilAvaliability
            {
                Id = 14,
                PupilId = _pupils[6].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new PupilAvaliability
            {
                Id = 15,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[6].Id
            },
            new PupilAvaliability
            {
                Id = 16,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[7].Id
            },
            new PupilAvaliability
            {
                Id = 17,
                PupilId = _pupils[7].Id,
                LessonSlotId = _lessonSlots[4].Id
            }
        };

        private static readonly Dictionary<int, int> _expectedTimetable = new()
        {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 4 },
            { 5, 5 },
            { 6, 6 },
            { 7, 7 }
        };

        #endregion

        public static bool IsPossible => _IS_POSSIBLE;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<LessonSlotData> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static Dictionary<int, int>? ExpectedTimetable => _expectedTimetable;

        public static IEnumerable<PupilAvaliability> PupilLessonSlots => _pupilLessonSlots;
    }
}