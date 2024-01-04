using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Tests.Timetabling
{
    internal class ImpossibleTestCase1 : ITimetableTestCase
    {
        private const bool _IS_POSSIBLE = false;

        #region Data

        private static readonly List<LessonSlotData> _lessonSlots = new()
        {
            new LessonSlotData
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 0,
                StartTime = new TimeSpan(09, 00, 00),
                EndTime = new TimeSpan(10, 00, 00)
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 1,
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(11, 00, 00)
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 2,
                StartTime = new TimeSpan(11, 00, 00),
                EndTime = new TimeSpan(12, 00, 00)
            },
            new LessonSlotData
            {
                Id = 3,
                DayOfWeek = DayOfWeek.Monday,
                FlagIndex = 3,
                StartTime = new TimeSpan(12, 00, 00),
                EndTime = new TimeSpan(13, 00, 00)
            }
        };

        private static readonly List<Pupil> _pupils = new()
        {
            new Pupil
            {
                Id = 0,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00),
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex)
            },
            new Pupil
            {
                Id = 1,
                NeedsDifferentTimes = false,
                LessonDuration = new TimeSpan(01, 00, 00),
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[1].FlagIndex)
            },
            new Pupil
            {
                Id = 2,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00),
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[1].FlagIndex, _lessonSlots[2].FlagIndex, _lessonSlots[3].FlagIndex)
            },
            new Pupil
            {
                Id = 3,
                NeedsDifferentTimes = true,
                LessonDuration = new TimeSpan(01, 00, 00),
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[1].FlagIndex)
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
                LessonSlotId = 3
            },
        };

        private static readonly Dictionary<int, int> _expectedTimeTable = new()
        {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 }
        };

        #endregion

        public static bool IsPossible => _IS_POSSIBLE;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<LessonSlotData> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static Dictionary<int, int>? ExpectedTimetable => _expectedTimeTable;
    }
}