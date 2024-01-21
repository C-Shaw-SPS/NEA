using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Tests.Timetabling
{
    public interface ITimetableTestCase
    {
        public abstract static bool IsPossible { get; }

        public abstract static IEnumerable<Pupil> Pupils { get; }

        public abstract static IEnumerable<LessonSlotData> LessonSlots { get; }

        public abstract static IEnumerable<LessonData> PrevLessons { get; }

        public abstract static Dictionary<int, int>? ExpectedTimetable { get; }
    }
}