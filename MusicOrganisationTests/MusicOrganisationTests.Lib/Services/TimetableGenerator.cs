using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Exceptions;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Tables;


namespace MusicOrganisationTests.Lib.Services
{
    public class TimetableGenerator
    {
        private static readonly Random _random = new();

        private readonly Dictionary<int, Pupil> _pupils;
        private readonly List<int> _shuffledPupilIds;
        private readonly Dictionary<int, LessonSlotData> _lessonSlots;
        private readonly Dictionary<int, int> _fixedLessons;
        private readonly Dictionary<int, int> _previousLessonSlots;
        private readonly Stack<(int pupilId, int lessonSlotId)> _stack;
        private readonly int _maximumLessonSlotId;
        private readonly Dictionary<int, int> _timetable;

        public TimetableGenerator(IEnumerable<Pupil> pupils, IEnumerable<LessonSlotData> lessonSlots, IEnumerable<LessonData> previousLessons)
        {
            _pupils = GetDictionary(pupils);
            _shuffledPupilIds = GetShuffledIdsOfVariableLessonPupils(_pupils.Values);
            _lessonSlots = GetDictionary(lessonSlots);
            _fixedLessons = GetFixedLessons(_pupils.Values, _lessonSlots.Values);
            _previousLessonSlots = GetPreviousLessonSlots(previousLessons);
            _stack = new();
            _maximumLessonSlotId = GetMaximumId(_lessonSlots.Values);
            _timetable = new();
        }

        /// <summary>
        /// Returns a dictionary with the ID of each value as the key
        /// </summary>
        private static Dictionary<int, TValue> GetDictionary<TValue>(IEnumerable<TValue> values) where TValue : IIdentifiable
        {
            Dictionary<int, TValue> dictionary = new();
            foreach (TValue value in values)
            {
                dictionary.Add(value.Id, value);
            }
            return dictionary;
        }

        /// <summary>
        /// Returns a dictionary of all pupils with a single lesson slot, with pupil ID as the key and lesson slot ID as the value
        /// </summary>
        private static Dictionary<int, int> GetFixedLessons(IEnumerable<Pupil> pupils, IEnumerable<LessonSlotData> lessonSlots)
        {
            Dictionary<(DayOfWeek dayOfWeek, int slotIndex), LessonSlotData> lessonSlotsFromDayAndIndex = GetLessonSlotsFromDayAndIndex(lessonSlots);
            Dictionary<int, int> fixedLessons = new();
            foreach (Pupil pupil in pupils)
            {
                if (pupil.HasFixedLessonSlot)
                {
                    (DayOfWeek, int) dayAndIndex = pupil.GetFixedLessonSlot();
                    LessonSlotData lessonSlot = lessonSlotsFromDayAndIndex[dayAndIndex];
                    fixedLessons.Add(pupil.Id, lessonSlot.Id);
                }
            }
            return fixedLessons;
        }

        /// <summary>
        /// Returns a dictionary of all lesson slots with the day of week and flag index as the key
        /// </summary>
        private static Dictionary<(DayOfWeek dayOfWeek, int flagIndex), LessonSlotData> GetLessonSlotsFromDayAndIndex(IEnumerable<LessonSlotData> lessonSlots)
        {
            Dictionary<(DayOfWeek dayOfWeek, int flagIndex), LessonSlotData> lessonSlotsFromDayAndIndex = new();
            foreach (LessonSlotData lessonSlot in lessonSlots)
            {
                lessonSlotsFromDayAndIndex.Add((lessonSlot.DayOfWeek, lessonSlot.FlagIndex), lessonSlot);
            }
            return lessonSlotsFromDayAndIndex;
        }

        /// <summary>
        /// Returns a dictionary of all previous lessons with the pupil ID as the key and the lesson slot ID as the value
        /// </summary>
        private static Dictionary<int, int> GetPreviousLessonSlots(IEnumerable<LessonData> previousLessonsList)
        {
            Dictionary<int, int> previousLessonSlots = new();
            foreach (LessonData lesson in previousLessonsList)
            {
                previousLessonSlots.Add(lesson.PupilId, lesson.LessonSlotId);
            }
            return previousLessonSlots;
        }

        /// <summary>
        /// Returns a shuffled list of the IDs of all pupils who do not have a fixed lesson slot
        /// </summary>
        private static List<int> GetShuffledIdsOfVariableLessonPupils(IEnumerable<Pupil> pupils)
        {
            List<int> ids = GetIdsOfVariableLessonPupils(pupils);
            Shuffle(ids);
            return ids;
        }

        /// <summary>
        /// Returns a list of the IDs of all pupils who do not have a fixed lesson slot
        /// </summary>
        private static List<int> GetIdsOfVariableLessonPupils(IEnumerable<Pupil> pupils)
        {
            List<int> ids = pupils
                .Where(p => !p.HasFixedLessonSlot && p.HasAnyLessonSlots)
                .Select(p => p.Id)
                .ToList();
            return ids;
        }

        /// <summary>
        /// Shuffles a list
        /// </summary>
        private static void Shuffle<T>(List<T> values)
        {
            for (int n = values.Count - 1; n > 1; --n)
            {
                int k = _random.Next(n);
                (values[n], values[k]) = (values[k], values[k]);
            }
        }

        /// <summary>
        /// Gets the maximum ID of the elements in values
        /// </summary>
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

                Pupil pupil = _pupils[pupilIndex];
                InsertNewPupil(pupil);
            }
        }

        private bool NoMoreSlotsAvaliable()
        {
            return _timetable.Count >= _lessonSlots.Count;
        }

        private void InsertNewPupil(Pupil pupil)
        {
            InsertPupil(pupil, 0);
        }

        private void InsertPupil(Pupil pupil, int minLessonSlotId)
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
            Pupil pupil = _pupils[pupilId];
            _timetable.Remove(pupilId);
            InsertPupil(pupil, previousLessonSlotId + 1);
        }

        private (bool succeeded, int lessonSlotId) GetLessonSlot(Pupil pupil, int minLessonSlotId)
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

        private bool CanHaveLessonInSlot(Pupil pupil, int lessonSlotId)
        {
            LessonSlotData lessonSlotData = _lessonSlots[lessonSlotId];
            return IsPupilAvaliable(pupil, lessonSlotData)
                && IsLongEnoughLessonSlot(pupil, lessonSlotData)
                && IsDifferentTimeIfRequired(pupil, lessonSlotData);
        }

        private static bool IsPupilAvaliable(Pupil pupil, LessonSlotData lessonSlotData)
        {
            return pupil.IsAvaliableInSlot(lessonSlotData.DayOfWeek, lessonSlotData.FlagIndex);
        }

        private static bool IsLongEnoughLessonSlot(Pupil pupil, LessonSlotData lessonSlotData)
        {
            return pupil.LessonDuration <= lessonSlotData.Duration;
        }

        private bool IsDifferentTimeIfRequired(Pupil pupil, LessonSlotData lessonSlotData)
        {
            return !pupil.NeedsDifferentTimes || _previousLessonSlots[pupil.Id] != lessonSlotData.Id;
        }
    }
}