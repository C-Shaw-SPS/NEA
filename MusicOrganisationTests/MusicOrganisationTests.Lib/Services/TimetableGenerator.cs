using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Tables;
using System.Runtime.CompilerServices;

namespace MusicOrganisationTests.Lib.Services
{
    public class TimetableGenerator
    {
        private static readonly Random _random = new();

        private readonly Dictionary<int, int> _timetable;
        private readonly Stack<(int lessonSlotId, int pupilId)> _stack;
        private readonly Dictionary<int, Pupil> _pupils;
        private readonly Dictionary<int, LessonSlotData> _lessonSlots;
        private readonly Dictionary<int, int> _prevTimetable;
        private readonly List<(int lessonSlotId, int pupilId)> _fixedLessons;
        private readonly int _maxPupilId;
        private readonly List<int> _variableLessons;

        public TimetableGenerator(IEnumerable<Pupil> pupils, IEnumerable<LessonSlotData> lessonSlots, IEnumerable<LessonData> prevLessons)
        {
            _timetable = [];
            _stack = [];
            _pupils = GetDictionary(pupils);
            _lessonSlots = GetDictionary(lessonSlots);
            _prevTimetable = GetTimetable(prevLessons);
            _fixedLessons = GetFixedLessons(pupils, lessonSlots);
            _maxPupilId = GetMaxId(pupils);
            _variableLessons = GetVariableLessons(lessonSlots, _fixedLessons);
        }

        #region Setup

        private static Dictionary<int, T> GetDictionary<T>(IEnumerable<T> values) where T : ITable
        {
            Dictionary<int, T> dict = [];
            foreach (T value in values)
            {
                dict.Add(value.Id, value);
            }
            return dict;
        }

        private static Dictionary<int, int> GetTimetable(IEnumerable<LessonData> lessons)
        {
            Dictionary<int, int> timetable = [];
            foreach (LessonData lesson in lessons)
            {
                timetable.Add(lesson.LessonSlotId, lesson.PupilId);
            }
            return timetable;
        }

        private List<(int lessonSlotId, int pupilId)> GetFixedLessons(IEnumerable<Pupil> pupils, IEnumerable<LessonSlotData> lessonSlots)
        {
            List<(int lessonSlotId, int pupilId)> fixedLessons = [];
            Dictionary<(DayOfWeek dayOfWeek, int flagIndex), int> lessonSlotsFromDayAndIndex = GetLessonSlotsFromDayAndIndex(lessonSlots);
            foreach (Pupil pupil in pupils)
            {
                if (pupil.HasFixedLessonSlot())
                {
                    (DayOfWeek dayOfWeek, int flagIndex) lessonSlot = pupil.GetFixedLessonSlot();
                    int lessonSlotId = lessonSlotsFromDayAndIndex[lessonSlot];
                    fixedLessons.Add((lessonSlotId, pupil.Id));
                }
            }
            Shuffle(fixedLessons);
            return fixedLessons;
        }

        private Dictionary<(DayOfWeek dayOfWeek, int flagIndex), int> GetLessonSlotsFromDayAndIndex(IEnumerable<LessonSlotData> lessonSlots)
        {
            Dictionary<(DayOfWeek dayOfWeek, int flagIndex), int> lessonSlotsFromDayAndIndex = [];
            foreach (LessonSlotData lessonSlot in lessonSlots)
            {
                lessonSlotsFromDayAndIndex.Add((lessonSlot.DayOfWeek, lessonSlot.FlagIndex), lessonSlot.Id);
            }
            return lessonSlotsFromDayAndIndex;
        }

        private static int GetMaxId<T>(IEnumerable<T> values) where T : ITable
        {
            return values.Max(v => v.Id);
        }

        private static List<int> GetVariableLessons(IEnumerable<LessonSlotData> lessonSlots, List<(int lessonSlotId, int pupilId)> fixedLessonSlots)
        {
            List<int> variableLessonSlots = [];
            foreach (LessonSlotData lessonSlot in lessonSlots)
            {
                if (!fixedLessonSlots.Any(fixedSlot => fixedSlot.lessonSlotId == lessonSlot.Id))
                {
                    variableLessonSlots.Add(lessonSlot.Id);
                }
            }
            Shuffle(variableLessonSlots);
            return variableLessonSlots;
        }

        private static void Shuffle<T>(List<T> values)
        {
            for (int n = values.Count - 1; n > 1; --n)
            {
                int k = _random.Next(n);
                (values[n], values[k]) = (values[k], values[n]);
            }
        }

        #endregion

        #region Timetabling

        public bool TryGenerateTimetable(out Dictionary<int, int> timetable)
        {
            bool suceeded = true;
            suceeded &= TryInsertFixedLessons();
            suceeded &= TryInsertVariableLessons();
            timetable = new(_timetable);
            return suceeded;
        }

        private bool TryInsertFixedLessons()
        {
            bool suceeded = true;
            foreach ((int lessonSlotId, int pupilId) in _fixedLessons)
            {
                suceeded &= _timetable.TryAdd(lessonSlotId, pupilId);
            }
            return suceeded;
        }

        private bool TryInsertVariableLessons()
        {
            foreach (int lessonSlotId in _variableLessons)
            {
                if (_timetable.Count == _pupils.Count)
                {
                    return true;
                }
                TryFillLessonSlot(lessonSlotId, 0);
            }

            return _timetable.Count == _pupils.Count;
        }

        private bool TryFillLessonSlot(int lessonSlotId, int minPupilId)
        {
            LessonSlotData lessonSlot = _lessonSlots[lessonSlotId];
            bool canFillInCurrentState = TryGetValidPupilId(lessonSlot, minPupilId, out int pupilId);
            if (canFillInCurrentState)
            {
                _timetable.Add(lessonSlot.Id, pupilId);
                _stack.Push((lessonSlot.Id, pupilId));
                return true;
            }
            
            while (TryMoveState())
            {
                canFillInCurrentState = TryGetValidPupilId(lessonSlot, 0, out pupilId);
                if (canFillInCurrentState)
                {
                    _timetable.Add(lessonSlot.Id, pupilId);
                    _stack.Push((lessonSlot.Id, pupilId));
                    return true;
                }
            }

            return false;
        }

        private bool TryGetValidPupilId(LessonSlotData lessonSlot, int minPupilId, out int pupilId)
        {
            for (pupilId = minPupilId; pupilId <= _maxPupilId; ++pupilId)
            {
                if (IsValidPupilIdForLessonSlot(pupilId, lessonSlot))
                {
                    return true;
                }
            }
            return false;
        }

        private bool TryMoveState()
        {
            if (_stack.Count == 0)
            {
                return false;
            }

            (int lessonSlotId, int pupilId) = _stack.Pop();
            _timetable.Remove(lessonSlotId);
            bool suceeded = TryFillLessonSlot(lessonSlotId, pupilId + 1);
            return suceeded;
        }

        #endregion

        #region Lesson Slot Validation

        public bool IsValidPupilIdForLessonSlot(int pupilId, LessonSlotData lessonSlot)
        {
            if (_timetable.ContainsKey(pupilId))
            {
                return false;
            }
            else if (_pupils.TryGetValue(pupilId, out Pupil? pupil))
            {
                return IsValidPupilForLessonSlot(pupil, lessonSlot);
            }
            else
            {
                return false;
            }
        }

        private bool IsValidPupilForLessonSlot(Pupil pupil, LessonSlotData lessonSlot)
        {
            return pupil.IsAvaliableInSlot(lessonSlot)
                && IsLongEnoughLessonSlot(pupil, lessonSlot)
                && IsDifferentSlotIfNeeded(pupil, lessonSlot);
        }

        private static bool IsLongEnoughLessonSlot(Pupil pupil, LessonSlotData lessonSlot)
        {
            return pupil.LessonDuration <= lessonSlot.Duration;
        }

        private bool IsDifferentSlotIfNeeded(Pupil pupil, LessonSlotData lessonSlot)
        {
            return !(pupil.NeedsDifferentTimes
                && _prevTimetable.TryGetValue(lessonSlot.Id, out int prebPupilId)
                && prebPupilId == pupil.Id);
        }

        #endregion
    }
}