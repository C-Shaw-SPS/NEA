using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    public interface ITimetableTestCase
    {
        public abstract static bool IsPossible { get; }

        public abstract static IEnumerable<Pupil> Pupils { get; }

        public abstract static IEnumerable<PupilAvailability> PupilAvailabilities { get; }

        public abstract static IEnumerable<LessonSlotData> LessonSlots { get; }

        public abstract static IEnumerable<LessonData> PrevLessons { get; }

        public abstract static Dictionary<int, int>? ExpectedTimetable { get; }

        public static DateTime GetDateFromDayOfWeek(DayOfWeek dayOfWeek)
        {
            DateTime dateTime = DateTime.Today;
            while (dateTime.DayOfWeek != dayOfWeek)
            {
                dateTime += TimeSpan.FromDays(1);
            }
            return dateTime;
        }
    }
}