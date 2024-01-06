using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Lib.Services
{
    internal class TimetableGenerator
    {
        private static readonly Random _random = new();

        private Dictionary<int, int> _timetable;
        private Stack<(int lessonSlotId, Pupil pupil)> _stack;
        private readonly Dictionary<int, LessonSlotData> _lessonSlots;
        private readonly Dictionary<int, int> _prevTimetable;
        private readonly List<Pupil> _fixedLessonPupils;
        private readonly List<Pupil> _variableLessonPupils;
        private readonly int _maxLessonSlotId;

        public TimetableGenerator(IEnumerable<Pupil> pupils, IEnumerable<LessonSlotData> lessonSlots, IEnumerable<LessonData> prevLessons)
        {
            _timetable = [];
            _stack = [];
            _lessonSlots = GetDictionary(lessonSlots);
            _prevTimetable = GetTimetable(prevLessons);
            _fixedLessonPupils = GetFixedLessonPupils(pupils);
            _variableLessonPupils = GetVariableLessonPupils(pupils);
            _maxLessonSlotId = GetMaxId(lessonSlots);
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

        private static int GetMaxId<T>(IEnumerable<T> values) where T : ITable
        {
            return values.Max(v => v.Id);
        }

        private static List<Pupil> GetFixedLessonPupils(IEnumerable<Pupil> pupils)
        {
            List<Pupil> fixedLessonPupils = [];
            foreach (Pupil pupil in pupils)
            {
                if (pupil.HasFixedLessonSlot())
                {
                    fixedLessonPupils.Add(pupil);
                }
            }
            Shuffle(fixedLessonPupils);
            return fixedLessonPupils;
        }

        private static List<Pupil> GetVariableLessonPupils(IEnumerable<Pupil> pupils)
        {
            List<Pupil> variableLessonPupils = [];
            foreach (Pupil pupil in pupils)
            {
                if (pupil.HasVariableLessonSlots())
                {
                    variableLessonPupils.Add(pupil);
                }
            }
            Shuffle(variableLessonPupils);
            return variableLessonPupils;
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
            suceeded &= TryInsertFixedLessonPupils();
            suceeded &= TryInsertVariableLessonPupils();
            timetable = _timetable;
            return suceeded;
        }

        private bool TryInsertFixedLessonPupils()
        {
            bool succeeded = true;
            foreach (Pupil pupil in _fixedLessonPupils)
            {
                succeeded &= TryInsertFixedLessonPupil(pupil);
            }
            return succeeded;
        }

        private bool TryInsertFixedLessonPupil(Pupil pupil)
        {
            bool suceeded = TryGetValidLessonSlot(pupil, 0, out int lessonSlotId);
            if (suceeded)
            {
                _timetable.Add(lessonSlotId, pupil.Id);
            }
            return suceeded;
        }

        private bool TryInsertVariableLessonPupils()
        {
            bool suceeded = true;
            foreach (Pupil pupil in _variableLessonPupils)
            {
                suceeded &= TryInsertVariableLessonPupil(pupil);
            }
            return suceeded;
        }

        private bool TryInsertVariableLessonPupil(Pupil pupil)
        {
            Dictionary<int, int> currentTimetable = new(_timetable);
            Stack<(int lessonSlotId, Pupil pupil)> currentStack = new(_stack);
            bool suceeded = TryInsertPupil(pupil, 0);
            if (!suceeded)
            {
                _timetable = currentTimetable;
                _stack = currentStack;
            }
            return suceeded;
        }

        private bool TryInsertPupil(Pupil pupil, int minLessonSlotId)
        {
            bool canInsert = TryGetValidLessonSlot(pupil, minLessonSlotId, out int lessonSlotId);
            if (canInsert)
            {
                _timetable.Add(lessonSlotId, pupil.Id);
                _stack.Push((lessonSlotId, pupil));
                return true;
            }

            while (TryMoveState())
            {
                canInsert = TryGetValidLessonSlot(pupil, 0, out lessonSlotId);
                if (canInsert)
                {
                    _timetable.Add(lessonSlotId, pupil.Id);
                    _stack.Push((lessonSlotId, pupil));
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

            (int lessonSlotId, Pupil pupil) = _stack.Pop();
            _timetable.Remove(lessonSlotId);
            bool succeeded = TryInsertPupil(pupil, lessonSlotId + 1);
            return succeeded;
        }

        private bool TryGetValidLessonSlot(Pupil pupil, int minLessonSlotId, out int lessonSlotId)
        {
            for (lessonSlotId = minLessonSlotId; lessonSlotId <= _maxLessonSlotId; ++lessonSlotId)
            {
                if (IsValidLessonSlotIdForPupil(lessonSlotId, pupil))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Lesson Slot Validation

        private bool IsValidLessonSlotIdForPupil(int lessonSlotId, Pupil pupil)
        {
            if (_timetable.ContainsKey(lessonSlotId))
            {
                return false;
            }
            else if (_lessonSlots.TryGetValue(lessonSlotId, out LessonSlotData? lessonSlot))
            {
                return IsValidLessonSlotForPupil(lessonSlot, pupil);
            }
            else
            {
                return false;
            }
        }

        private bool IsValidLessonSlotForPupil(LessonSlotData lessonSlot, Pupil pupil)
        {
            return pupil.IsAvaliableInSlot(lessonSlot)
                && IsLongEnoughLessonSlot(lessonSlot, pupil)
                && IsDifferentSlotIfNeeded(lessonSlot, pupil);
        }

        private static bool IsLongEnoughLessonSlot(LessonSlotData lessonSlot, Pupil pupil)
        {
            return pupil.LessonDuration <= lessonSlot.Duration;
        }

        private bool IsDifferentSlotIfNeeded(LessonSlotData lessonSlot, Pupil pupil)
        {
            return !(pupil.NeedsDifferentTimes
                && _prevTimetable.TryGetValue(lessonSlot.Id, out int prebPupilId)
                && prebPupilId == pupil.Id);
        }

        #endregion
    }
}