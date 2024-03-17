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
        public void TestImpossibleCase()
        {
            TestCase<ImpossibleTestCase>();
        }

        [Fact]
        public void TestLargeCase()
        {
            TestCase<LargeTestCase>();
        }

        private static void TestCase<T>() where T : ITimetableTestCase
        {
            TimetableGenerator timetableGenerator = new(T.Pupils, T.PupilAvailabilities, T.LessonSlots, T.PrevLessons);
            bool succeeded = timetableGenerator.TryGenerateTimetable(out Dictionary<int, int> actualTimetable);
            Assert.Equal(T.IsPossible, succeeded);

            foreach (int lessonSlotId in T.ExpectedTimetable.Keys)
            {
                Assert.True(actualTimetable.TryGetValue(lessonSlotId, out int pupilId));
                Assert.Equal(T.ExpectedTimetable[lessonSlotId], pupilId);
            }
        }
    }
}