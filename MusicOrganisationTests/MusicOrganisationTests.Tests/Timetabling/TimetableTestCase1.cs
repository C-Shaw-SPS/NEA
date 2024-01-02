using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.App.TimetableTestCases
{
    public class TimetableTestCase1 : ITimetableTestCase
    {
        private static List<LessonSlotData> _lessonSlots = new()
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

        private static List<Pupil> _pupils = new()
        {
            new Pupil
            {
                Id = 0,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex)
            },
            new Pupil
            {
                Id = 1,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = false,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[1].FlagIndex)
            },
            new Pupil
            {
                Id = 2,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = true,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[2].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[4].FlagIndex, _lessonSlots[6].FlagIndex)
            },
            new Pupil
            {
                Id = 3,
                LessonDuration = new TimeSpan(00,45,00),
                NeedsDifferentTimes = true,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[1].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[6].FlagIndex, _lessonSlots[7].FlagIndex)
            },
            new Pupil
            {
                Id = 4,
                LessonDuration = new TimeSpan(00,30,00),
                NeedsDifferentTimes = true,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[3].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[4].FlagIndex, _lessonSlots[5].FlagIndex)
            },
            new Pupil
            {
                Id = 5,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false,
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[5].FlagIndex)
            },
            new Pupil
            {
                Id = 6,
                LessonDuration = new TimeSpan(00,45,00),
                NeedsDifferentTimes = true,
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[4].FlagIndex, _lessonSlots[5].FlagIndex, _lessonSlots[6].FlagIndex, _lessonSlots[7].FlagIndex)
            },
            new Pupil
            {
                Id = 7,
                LessonDuration = new TimeSpan(01,00,00),
                NeedsDifferentTimes = false,
                MondayLessonSlots = Flags.GetNewFlags(_lessonSlots[0].FlagIndex, _lessonSlots[1].FlagIndex, _lessonSlots[2].FlagIndex, _lessonSlots[3].FlagIndex),
                TuesdayLessonSlots = Flags.GetNewFlags(_lessonSlots[6].FlagIndex)
            }
        };

        private static List<LessonData> _prevLessons = new()
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

        private const string _DATABASE_NAME = "TimetableTestCase1.db";

        private const bool _IS_POSSIBLE = true;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<LessonSlotData> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static string DatabaseName => _DATABASE_NAME;

        public static bool IsPossible => _IS_POSSIBLE;
    }
}