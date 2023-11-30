using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Exceptions;
using MusicOrganisationTests.Lib.Tables;


namespace MusicOrganisationTests.Lib.Services
{
    public class TimetableGenerator
    {
        private static readonly Random _random = new();

        private readonly Dictionary<int, PupilData> _pupils;
        private readonly List<int> _shuffledPupilIds;
        private readonly Dictionary<int, LessonSlotData> _lessonSlots;
        private readonly Dictionary<int, int> _fixedLessons;
        private readonly Dictionary<int, int> _previousLessonSlots;
        private readonly Stack<(int pupilId, int lessonSlotId)> _stack;
        private readonly int _maximumLessonSlotId;
        private readonly Dictionary<int, int> _timetable;

        public TimetableGenerator(IEnumerable<PupilData> pupils, IEnumerable<LessonSlotData> lessonSlots, IEnumerable<LessonData> previousLessons, IEnumerable<FixedLessonData> fixedLessons)
        {
            _pupils = GetDictionary(pupils);
            _shuffledPupilIds = GetShuffledIdsOfNonFixedLessons(_pupils.Values);
            _lessonSlots = GetDictionary(lessonSlots);
            _fixedLessons = GetFixedLessons(fixedLessons);
            _previousLessonSlots = GetPreviousLessonSlots(previousLessons);
            _stack = new();
            _maximumLessonSlotId = GetMaximumId(_lessonSlots.Values);
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

        private static Dictionary<int, int> GetFixedLessons(IEnumerable<FixedLessonData> fixedLessonsList)
        {
            Dictionary<int, int> fixedLessons = new();
            foreach (FixedLessonData fixedLesson in fixedLessonsList)
            {
                fixedLessons.Add(fixedLesson.PupilId, fixedLesson.LessonSlotId);
            }
            return fixedLessons;
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

        private List<int> GetShuffledIdsOfNonFixedLessons(IEnumerable<PupilData> pupils)
        {
            List<int> ids = pupils
                .Where(p => !_fixedLessons.ContainsKey(p.Id))
                .Select(p => p.Id)
                .ToList();
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
            InsertFixedLessons();
            InsertVariableLessons();
        }

        private void InsertFixedLessons()
        {
            foreach (int pupilId in _fixedLessons.Keys)
            {
                _timetable.Add(_fixedLessons[pupilId], pupilId);
            }
        }

        private void InsertVariableLessons()
        {
            for (int pupilIndex = 0; pupilIndex < _shuffledPupilIds.Count; ++pupilIndex)
            {
                if (NoMoreSlotsAvaliable())
                {
                    return;
                }

                PupilData pupil = _pupils[pupilIndex];
                InsertNewPupil(pupil);
            }
        }

        private bool NoMoreSlotsAvaliable()
        {
            return _timetable.Count >= _lessonSlots.Count;
        }

        private void InsertNewPupil(PupilData pupil)
        {
            InsertPupil(pupil, 0);
        }

        private void InsertPupil(PupilData pupil, int minLessonSlotId)
        {
            bool succeeded;
            do
            {
                (succeeded, int newLessonSlotId) = GetLessonSlot(pupil, minLessonSlotId);
                if (succeeded)
                {
                    _timetable.Add(pupil.Id, newLessonSlotId);
                }
                else
                {
                    MovePreviousPupil();
                }
            } while (!succeeded);
        }

        private void MovePreviousPupil()
        {
            (int pupilId, int previousLessonSlotId) = _stack.Pop();
            PupilData pupil = _pupils[pupilId];
            _timetable.Remove(pupilId);
            InsertPupil(pupil, previousLessonSlotId + 1);
        }

        private (bool succeeded, int lessonSlotId) GetLessonSlot(PupilData pupil, int minLessonSlotId)
        {
            for (int lessonSlotId = minLessonSlotId; lessonSlotId <= _maximumLessonSlotId; ++lessonSlotId)
            {
                if (!IsValidLessonSlotId(lessonSlotId))
                {
                    continue;
                }
                if (CanHaveLessonInSlot(pupil, lessonSlotId))
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

        private bool CanHaveLessonInSlot(PupilData pupil, int lessonSlotId)
        {
            LessonSlotData lessonSlotData = _lessonSlots[lessonSlotId];
            return IsPupilAvaliable(pupil, lessonSlotData)
                && IsLongEnoughLessonSlot(pupil, lessonSlotData)
                && IsDifferentTimeIfRequired(pupil, lessonSlotData);
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