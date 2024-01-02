using MusicOrganisationTests.App.TimetableTestCases;
using MusicOrganisationTests.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Tests.Timetabling
{
    public class TimetableTests
    {
        [Fact]
        public void TestCase1()
        {
            TestCase<TimetableTestCase2>();
        }

        [Fact]
        public void TestCase2()
        {
            TestCase<TimetableTestCase1>();
        }

        private static void TestCase<T>() where T : ITimetableTestCase
        {
            TimetableGenerator timetableGenerator = new(T.Pupils, T.LessonSlots, T.PrevLessons);
            bool suceeded = timetableGenerator.TryGenerateTimetable(out Dictionary<int, int> timetable);
            Assert.Equal(T.IsPossible, suceeded);
            if (T.ExpectedTimetable is not null)
            {
                foreach (int lessonSlotId in T.ExpectedTimetable.Keys)
                {
                    Assert.True(timetable.TryGetValue(lessonSlotId, out int pupilId));
                    Assert.Equal(T.ExpectedTimetable[lessonSlotId], pupilId);
                }
            }
        }
    }
}