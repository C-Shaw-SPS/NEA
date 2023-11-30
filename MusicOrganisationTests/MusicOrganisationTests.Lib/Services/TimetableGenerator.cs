using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Exceptions;
using MusicOrganisationTests.Lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationTests.Lib.Services
{
    public class TimetableGenerator
    {
        private static readonly Random _random = new();

        private readonly Dictionary<int, PupilData> _pupils;
        private readonly List<int> _shuffledPupilIds;
        private readonly Dictionary<int, LessonSlotData> _lessonSlots;
        private readonly Dictionary<int, int> _previousLessonSlots;
        private readonly Stack<(int pupilId, int lessonSlotIndex)> _stack;
        private readonly int _maximumLessonSlotId;
        private readonly Dictionary<int, int> _timetable;

        public TimetableGenerator(IEnumerable<PupilData> pupilsList, IEnumerable<LessonSlotData> lessonSlotsList, IEnumerable<LessonData> previousLessons)
        {
            _pupils = GetDictionary(pupilsList);
            _shuffledPupilIds = GetShuffledIds(pupilsList);
            _lessonSlots = GetDictionary(lessonSlotsList);
            _previousLessonSlots = GetPreviousLessonSlots(previousLessons);
            _stack = new();
            _maximumLessonSlotId = GetMaximumId(lessonSlotsList);
            _timetable = new();
        }

        private static Dictionary<int, TValue> GetDictionary<TValue>(IEnumerable<TValue> values) where TValue : ITable
        {
            Dictionary<int, TValue> dictionary = new();
            foreach (TValue value in values)
            {
                dictionary.Add(value.Id, value);
            }
            return dictionary;
        }

        private static Dictionary<int, int> GetPreviousLessonSlots(IEnumerable<LessonData> previousLessonsList)
        {
            Dictionary<int, int> previousLessonSlots = new();
            foreach (LessonData lesson in previousLessonsList)
            {
                previousLessonSlots.Add(lesson.PupilId, lesson.LessonSlotId);
            }
            return previousLessonSlots;
        }

        private static List<int> GetShuffledIds<T>(IEnumerable<T> values) where T : ITable
        {
            List<int> ids = values.Select(v => v.Id).ToList();
            for (int n = ids.Count - 1; n > 1; --n)
            {
                int k = _random.Next(n);
                (ids[n], ids[k]) = (ids[k], ids[n]);
            }
            return ids;
        }

        private static int GetMaximumId<T>(IEnumerable<T> values) where T : ITable
        {
            return values.Max(v => v.Id);
        }

        public void GenerateTimetable()
        {

        }

        private (bool succeeded, int lessonSlotId) GetLessonSlot(int pupilId, int startAfterId)
        {
            for (int lessonSlotId = startAfterId + 1; lessonSlotId <= _maximumLessonSlotId; ++lessonSlotId)
            {
                if (!IsValidLessonSlotId(lessonSlotId))
                {
                    continue;
                }
                if (CanHaveLessonInSlot(pupilId, lessonSlotId))
                {
                    return (true, lessonSlotId);
                }
            }
            return (false, 0);
        }

        private bool IsValidLessonSlotId(int lessonSlotId)
        {
            return _lessonSlots.ContainsKey(lessonSlotId);
        }

        private bool CanHaveLessonInSlot(int pupilId, int lessonSlotId)
        {
            PupilData pupilData = _pupils[pupilId];
            LessonSlotData lessonSlotData = _lessonSlots[lessonSlotId];
            return IsPupilAvaliable(pupilData, lessonSlotData)
                && IsLongEnoughLessonSlot(pupilData, lessonSlotData)
                && IsDifferentTimeIfRequired(pupilData, lessonSlotData);
        }

        private bool IsPupilAvaliable(PupilData pupilData, LessonSlotData lessonSlotData)
        {
            return lessonSlotData.DayOfWeek switch
            {
                DayOfWeek.Monday => pupilData.MondayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Tuesday => pupilData.TuesdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Wednesday => pupilData.WednesdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Thursday => pupilData.ThursdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Friday => pupilData.FridayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Saturday => pupilData.SaturdayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                DayOfWeek.Sunday => pupilData.SundayLessonSlots.HasFlagAtIndex(lessonSlotData.FlagIndex),
                _ => throw new DayOfWeekException(lessonSlotData.DayOfWeek),
            };
        }

        private bool IsLongEnoughLessonSlot(PupilData pupilData, LessonSlotData lessonSlotData)
        {
            return pupilData.LessonDuration <= lessonSlotData.Duration;
        }

        private bool IsDifferentTimeIfRequired(PupilData pupilData, LessonSlotData lessonSlotData)
        {
            return !pupilData.NeedsDifferentTimes || _previousLessonSlots[pupilData.Id] != lessonSlotData.Id;
        }
    }
}