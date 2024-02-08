using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    internal class AllDifferentDurationsTestCase : ITimetableTestCase
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
                EndTime = new TimeSpan(09, 15, 00)
            },
            new LessonSlotData
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(10, 30, 00)
            },
            new LessonSlotData
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(11, 00, 00),
                EndTime = new TimeSpan(11, 45, 00)
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
                NeedsDifferentTimes = false,
                LessonDuration = _lessonSlots[0].Duration,
            },
            new Pupil
            {
                Id = 1,
                NeedsDifferentTimes = false,
                LessonDuration = _lessonSlots[1].Duration,
            },
            new Pupil
            {
                Id = 2,
                NeedsDifferentTimes = false,
                LessonDuration = _lessonSlots[2].Duration,
            },
            new Pupil
            {
                Id = 3,
                NeedsDifferentTimes = false,
                LessonDuration = _lessonSlots[3].Duration,
            }
        };

        private static readonly List<LessonData> _prevLessons = new()
        {
            new LessonData
            {
                PupilId = _pupils[0].Id,
                StartTime = _lessonSlots[0].StartTime,
                EndTime = _lessonSlots[0].EndTime,
                Date = ITimetableTestCase.GetDateFromDayOfWeek(_lessonSlots[0].DayOfWeek)
            },
            new LessonData
            {
                PupilId = _pupils[1].Id,
                StartTime = _lessonSlots[1].StartTime,
                EndTime = _lessonSlots[1].EndTime,
                Date = ITimetableTestCase.GetDateFromDayOfWeek(_lessonSlots[1].DayOfWeek)
            },
            new LessonData
            {
                PupilId = _pupils[2].Id,
                StartTime = _lessonSlots[2].StartTime,
                EndTime = _lessonSlots[2].EndTime,
                Date = ITimetableTestCase.GetDateFromDayOfWeek(_lessonSlots[2].DayOfWeek)
            },
            new LessonData
            {
                PupilId = _pupils[3].Id,
                StartTime = _lessonSlots[3].StartTime,
                EndTime = _lessonSlots[3].EndTime,
                Date = ITimetableTestCase.GetDateFromDayOfWeek(_lessonSlots[3].DayOfWeek)
            },
        };

        private static readonly Dictionary<int, int> _expectedTimetable = new()
        {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 3 }
        };

        private static readonly List<PupilAvailability> _pupilLessonSlots = GetPupilLessonSlots();

        private static List<PupilAvailability> GetPupilLessonSlots()
        {
            List<PupilAvailability> pupilLessonSlots = [];
            int id = 0;
            foreach (Pupil pupil in _pupils)
            {
                foreach (LessonSlotData lessonSlot in _lessonSlots)
                {
                    PupilAvailability pupilLessonSlot = new()
                    {
                        Id = id,
                        PupilId = pupil.Id,
                        LessonSlotId = lessonSlot.Id
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

        public static IEnumerable<PupilAvailability> PupilLessonSlots => _pupilLessonSlots;
    }
}
