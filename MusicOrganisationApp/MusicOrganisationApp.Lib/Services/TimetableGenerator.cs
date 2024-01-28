using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.Models;

namespace MusicOrganisationApp.Lib.Services
{
    public class TimetableGenerator
    {
        private static readonly Random _random = new();

        private Dictionary<int, int> _timetable;
        private Stack<(int lessonSlotId, int pupilId)> _stack;
        private readonly Dictionary<int, LessonSlotData> _lessonSlots;
        private readonly Dictionary<int, Pupil> _pupils;
        private readonly Dictionary<int, HashSet<int>> _pupilLessonSlots;
        private readonly List<int> _fixedLessonPupilIds;
        private readonly List<int> _variableLessonPupilIds;
        private readonly Dictionary<int, int> _prevTimetable;
        private readonly int _maxLessonSlotId;

        public TimetableGenerator(IEnumerable<Pupil> pupils, IEnumerable<PupilLessonSlotData> pupilLessonSlots, IEnumerable<LessonSlotData> lessonSlots, IEnumerable<LessonData> prevLessons)
        {
            _timetable = [];
            _stack = [];
            _lessonSlots = GetDictionary(lessonSlots);
            _pupils = GetDictionary(pupils);
            _pupilLessonSlots = GetPupilLessonSlots(pupilLessonSlots);
            _fixedLessonPupilIds = GetFixedLessonPupilIds(_pupilLessonSlots);
            _variableLessonPupilIds = GetVariableLessonPupilIds(_pupilLessonSlots);
            _prevTimetable = GetTimetable(prevLessons);
            _maxLessonSlotId = GetMaxId(lessonSlots);
        }

        #region Setup

        private static Dictionary<int, T> GetDictionary<T>(IEnumerable<T> values) where T : IIdentifiable
        {
            Dictionary<int, T> dict = [];
            foreach (T value in values)
            {
                dict.Add(value.Id, value);
            }
            return dict;
        }

        private static Dictionary<int, HashSet<int>> GetPupilLessonSlots(IEnumerable<PupilLessonSlotData> pupilLessonSlotList)
        {
            Dictionary<int, HashSet<int>> pupilLessonSlots = [];
            foreach (PupilLessonSlotData pupilLessonSlot in pupilLessonSlotList)
            {
                if (pupilLessonSlots.TryGetValue(pupilLessonSlot.PupilId, out HashSet<int>? lessonSlots))
                {
                    lessonSlots.Add(pupilLessonSlot.LessonSlotId);
                }
                else
                {
                    pupilLessonSlots[pupilLessonSlot.PupilId] = [pupilLessonSlot.LessonSlotId];
                }
            }
            return pupilLessonSlots;
        }

        private static List<int> GetFixedLessonPupilIds(Dictionary<int, HashSet<int>> pupilLessonSlots)
        {
            List<int> fixedLessonPupilIds = [];
            foreach (int pupilId in pupilLessonSlots.Keys)
            {
                if (pupilLessonSlots[pupilId].Count == 1)
                {
                    fixedLessonPupilIds.Add(pupilId);
                }
            }
            Shuffle(fixedLessonPupilIds);
            return fixedLessonPupilIds;
        }

        private static List<int> GetVariableLessonPupilIds(Dictionary<int, HashSet<int>> pupilLessonSlots)
        {
            List<int> variableLessonPupilIds = [];
            foreach (int pupilId in pupilLessonSlots.Keys)
            {
                if (pupilLessonSlots[pupilId].Count > 1)
                {
                    variableLessonPupilIds.Add(pupilId);
                }
            }
            Shuffle(variableLessonPupilIds);
            return variableLessonPupilIds;
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
            foreach (int pupilId in _fixedLessonPupilIds)
            {
                succeeded &= TryInsertFixedLessonPupil(pupilId);
            }
            return succeeded;
        }

        private bool TryInsertFixedLessonPupil(int pupilId)
        {
            bool suceeded = TryGetValidLessonSlot(pupilId, 0, out int lessonSlotId);
            if (suceeded)
            {
                _timetable.Add(lessonSlotId, pupilId);
            }
            return suceeded;
        }

        private bool TryInsertVariableLessonPupils()
        {
            bool suceeded = true;
            foreach (int pupilId in _variableLessonPupilIds)
            {
                suceeded &= TryInsertVariableLessonPupil(pupilId);
            }
            return suceeded;
        }

        private bool TryInsertVariableLessonPupil(int pupilId)
        {
            Dictionary<int, int> currentTimetable = new(_timetable);
            Stack<(int lessonSlotId, int pupilId)> currentStack = new(_stack);
            bool suceeded = TryInsertPupil(pupilId, 0);
            if (!suceeded)
            {
                _timetable = currentTimetable;
                _stack = currentStack;
            }
            return suceeded;
        }

        private bool TryInsertPupil(int pupilId, int minLessonSlotId)
        {
            bool canInsert = TryGetValidLessonSlot(pupilId, minLessonSlotId, out int lessonSlotId);
            if (canInsert)
            {
                _timetable.Add(lessonSlotId, pupilId);
                _stack.Push((lessonSlotId, pupilId));
                return true;
            }

            while (TryMoveState())
            {
                canInsert = TryGetValidLessonSlot(pupilId, 0, out lessonSlotId);
                if (canInsert)
                {
                    _timetable.Add(lessonSlotId, pupilId);
                    _stack.Push((lessonSlotId, pupilId));
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
            bool succeeded = TryInsertPupil(pupilId, lessonSlotId + 1);
            return succeeded;
        }

        private bool TryGetValidLessonSlot(int pupilId, int minLessonSlotId, out int lessonSlotId)
        {
            Pupil pupil = _pupils[pupilId];
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
            else
            {
                return IsValidLessonSlotForPupil(lessonSlotId, pupil);
            }
        }

        private bool IsValidLessonSlotForPupil(int lessonSlotId, Pupil pupil)
        {
            LessonSlotData lessonSlot = _lessonSlots[lessonSlotId];
            return IsPupilAvaliableInSlot(pupil, lessonSlot)
                && IsLongEnoughLessonSlot(pupil, lessonSlot)
                && IsDifferentSlotIfNeeded(pupil, lessonSlot);
        }

        private bool IsPupilAvaliableInSlot(Pupil pupil, LessonSlotData lessonSlot)
        {
            HashSet<int> avaliableLessonSlots = _pupilLessonSlots[pupil.Id];
            return avaliableLessonSlots.Contains(lessonSlot.Id);
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