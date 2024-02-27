using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    internal class AllFixedTestCase : ITimetableTestCase
    {
        private const bool _IS_POSSIBLE = true;

        #region Data

        private static readonly List<LessonSlot> _lessonSlots = new()
        {
            new LessonSlot
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(09, 00, 00),
                EndTime = new TimeSpan(10, 00, 00)
            },
            new LessonSlot
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(10, 00, 00),
                EndTime = new TimeSpan(11, 00, 00)
            },
            new LessonSlot
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(11, 00, 00),
                EndTime = new TimeSpan(12, 00, 00)
            },
            new LessonSlot
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
            }
        };

        private static readonly List<LessonData> _prevLessons = GetPrevLessons();

        private static readonly Dictionary<int, int> _expectedTimetable = new()
        {
            { 0, 0 },
            { 1, 1 },
            { 2, 2 },
            { 3, 3 }
        };

        private static readonly List<PupilAvailability> __pupilAvailabilities = GetPupilAvailabilities();

        private static List<LessonData> GetPrevLessons()
        {
            List<LessonData> prevLessons = [];
            for (int i = 0; i < _pupils.Count; ++i)
            {
                LessonData lesson = new()
                {
                    Id = i,
                    PupilId = _pupils[i].Id,
                    StartTime = _lessonSlots[i].StartTime,
                    EndTime = _lessonSlots[i].EndTime,
                    Date = ITimetableTestCase.GetDateFromDayOfWeek(_lessonSlots[i].DayOfWeek)
                };
                prevLessons.Add(lesson);
            }
            return prevLessons;
        }

        private static List<PupilAvailability> GetPupilAvailabilities()
        {
            List<PupilAvailability> pupilAvailabilities = [];
            for (int i = 0; i < _pupils.Count; ++i)
            {
                PupilAvailability pupilAvailability = new()
                {
                    Id = i,
                    PupilId = _pupils[i].Id,
                    LessonSlotId = _lessonSlots[i].Id
                };
                pupilAvailabilities.Add(pupilAvailability);
            }
            return pupilAvailabilities;
        }

        #endregion

        public static bool IsPossible => _IS_POSSIBLE;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<LessonSlot> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static Dictionary<int, int>? ExpectedTimetable => _expectedTimetable;

        public static IEnumerable<PupilAvailability> PupilAvailabilities => __pupilAvailabilities;
    }
}