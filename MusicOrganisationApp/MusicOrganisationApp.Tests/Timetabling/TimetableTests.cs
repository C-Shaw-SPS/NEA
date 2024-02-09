using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Tests.Timetabling
{
    public class TimetableTests
    {
        [Fact]
        public void TestGeneralCase1()
        {
            TestCase<GeneralTestCase1>();
        }

        [Fact]
        public void TestGeneralCase2()
        {
            TestCase<GeneralTestCase2>();
        }

        [Fact]
        public void TestAllFixed()
        {
            TestCase<AllFixedTestCase>();
        }

        [Fact]
        public void TestAllDifferentTimes()
        {
            TestCase<AllDifferentTimesTestCase>();
        }

        [Fact]
        public void TestAllDifferentDurations()
        {
            TestCase<AllDifferentDurationsTestCase>();
        }

        [Fact]
        public void TestImpossibleCase1()
        {
            TestCase<ImpossibleTestCase1>();
        }

        private static void TestCase<T>() where T : ITimetableTestCase
        {
            TimetableGenerator timetableGenerator = new(T.Pupils, T.PupilAvailabilities, T.LessonSlots, T.PrevLessons);
            bool suceeded = timetableGenerator.TryGenerateTimetable(out Dictionary<int, int> actualTimetable);
            Assert.Equal(T.IsPossible, suceeded);

            if (T.ExpectedTimetable is not null)
            {
                foreach (int lessonSlotId in T.ExpectedTimetable.Keys)
                {
                    Assert.True(actualTimetable.TryGetValue(lessonSlotId, out int pupilId));
                    Assert.Equal(T.ExpectedTimetable[lessonSlotId], pupilId);
                }
            }
        }
    }
}