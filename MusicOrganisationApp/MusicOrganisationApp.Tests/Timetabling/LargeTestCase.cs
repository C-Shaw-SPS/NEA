using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    internal class LargeTestCase : ITimetableTestCase
    {
        #region Data

        private const int _COUNT = 12;
        private const int _AVAILABILITY_COUNT = _COUNT * (_COUNT + 1) / 2;
        private const bool _IS_POSSIBLE = true;

        private static readonly List<Pupil> _pupils = GetPupils();
        private static readonly List<LessonSlot> _lessonSlots = GetLessonSlots();
        private static readonly List<PupilAvailability> _pupilAvailabilities = GetPupilAvailabilities();
        private static readonly Dictionary<int, int> _expectedTimetable = GetExpectedTimetable();
        private static readonly List<LessonData> _prevLessons = [];

        private static List<Pupil> GetPupils()
        {
            List<Pupil> pupils = new() { Capacity = _COUNT };
            for (int i = 0; i < _COUNT; ++i)
            {
                Pupil pupil = new()
                {
                    Id = i,
                    NeedsDifferentTimes = false,
                    LessonDuration = TimeSpan.Zero
                };
                pupils.Add(pupil);
            }
            return pupils;
        }

        private static List<LessonSlot> GetLessonSlots()
        {
            List<LessonSlot> lessonSlots = new() { Capacity = _COUNT };
            for (int i = 0; i < _COUNT; ++i)
            {
                LessonSlot lessonSlot = new()
                {
                    Id = i,
                    StartTime = TimeSpan.Zero,
                    EndTime = TimeSpan.Zero,
                    DayOfWeek = DayOfWeek.Monday
                };
                lessonSlots.Add(lessonSlot);
            }
            return lessonSlots;
        }

        private static List<PupilAvailability> GetPupilAvailabilities()
        {
            List<PupilAvailability> pupilAvailabilities = new() { Capacity = _AVAILABILITY_COUNT };
            int id = 0;
            for (int pupilId = 0; pupilId < _COUNT; ++pupilId)
            {
                for (int lessonSlotId = 0; lessonSlotId <= pupilId; ++lessonSlotId)
                {
                    PupilAvailability pupilAvailability = new()
                    {
                        Id = id,
                        PupilId = pupilId,
                        LessonSlotId = lessonSlotId
                    };
                    pupilAvailabilities.Add(pupilAvailability);
                    ++id;
                }
            }
            return pupilAvailabilities;
        }

        private static Dictionary<int, int> GetExpectedTimetable()
        {
            Dictionary<int, int> expectedTimetable = [];
            for (int i = 0; i < _COUNT; ++i)
            {
                expectedTimetable[i] = i;
            }
            return expectedTimetable;
        }

        #endregion

        public static bool IsPossible => _IS_POSSIBLE;

        public static IEnumerable<Pupil> Pupils => _pupils;

        public static IEnumerable<PupilAvailability> PupilAvailabilities => _pupilAvailabilities;

        public static IEnumerable<LessonSlot> LessonSlots => _lessonSlots;

        public static IEnumerable<LessonData> PrevLessons => _prevLessons;

        public static Dictionary<int, int> ExpectedTimetable => _expectedTimetable;
    }
}