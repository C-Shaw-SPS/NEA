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
        private readonly Dictionary<int, HashSet<int>> _pupilAvailabilities;
        private readonly List<int> _fixedLessonPupilIds;
        private readonly List<int> _variableLessonPupilIds;
        private readonly Dictionary<int, LessonData> _prevTimetable;
        private readonly int _maxLessonSlotId;

        public TimetableGenerator(IEnumerable<Pupil> pupils, IEnumerable<PupilAvailability> pupilAvailabilities, IEnumerable<LessonSlotData> lessonSlots, IEnumerable<LessonData> prevLessons)
        {
            _timetable = [];
            _stack = [];
            _lessonSlots = lessonSlots.GetDictionary();
            _pupils = pupils.GetDictionary();
            _pupilAvailabilities = GetPupilAvailabilities(pupilAvailabilities);
            _fixedLessonPupilIds = GetFixedLessonPupilIds(_pupilAvailabilities);
            _variableLessonPupilIds = GetVariableLessonPupilIds(_pupilAvailabilities);
            _prevTimetable = GetPrevTimetable(prevLessons);
            _maxLessonSlotId = GetMaxId(lessonSlots);
        }

        #region Setup

        private static Dictionary<int, HashSet<int>> GetPupilAvailabilities(IEnumerable<PupilAvailability> pupilAvailabilityEnumerable)
        {
            Dictionary<int, HashSet<int>> pupilAvailabilities = [];
            foreach (PupilAvailability pupilAvailability in pupilAvailabilityEnumerable)
            {
                if (pupilAvailabilities.TryGetValue(pupilAvailability.PupilId, out HashSet<int>? lessonSlots))
                {
                    lessonSlots.Add(pupilAvailability.LessonSlotId);
                }
                else
                {
                    pupilAvailabilities[pupilAvailability.PupilId] = [pupilAvailability.LessonSlotId];
                }
            }
            return pupilAvailabilities;
        }

        private static List<int> GetFixedLessonPupilIds(Dictionary<int, HashSet<int>> pupilAvailabilities)
        {
            List<int> fixedLessonPupilIds = [];
            foreach (int pupilId in pupilAvailabilities.Keys)
            {
                if (pupilAvailabilities[pupilId].Count == 1)
                {
                    fixedLessonPupilIds.Add(pupilId);
                }
            }
            Shuffle(fixedLessonPupilIds);
            return fixedLessonPupilIds;
        }

        private static List<int> GetVariableLessonPupilIds(Dictionary<int, HashSet<int>> pupilAvailabilities)
        {
            List<int> variableLessonPupilIds = [];
            foreach (int pupilId in pupilAvailabilities.Keys)
            {
                if (pupilAvailabilities[pupilId].Count > 1)
                {
                    variableLessonPupilIds.Add(pupilId);
                }
            }
            Shuffle(variableLessonPupilIds);
            return variableLessonPupilIds;
        }

        private static Dictionary<int, LessonData> GetPrevTimetable(IEnumerable<LessonData> lessons)
        {
            Dictionary<int, LessonData> timetable = [];
            foreach (LessonData lesson in lessons)
            {
                timetable.Add(lesson.PupilId, lesson);
            }
            return timetable;
        }

        private static int GetMaxId<T>(IEnumerable<T> values) where T : IIdentifiable
        {
            if (values.Any())
            {
                return values.Max(v => v.Id);
            }
            else
            {
                return 0;
            }
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
            if (_lessonSlots.TryGetValue(lessonSlotId, out LessonSlotData? lessonSlot))
            {
                return IsPupilAvaliableInSlot(pupil, lessonSlot)
                    && IsLongEnoughLessonSlot(pupil, lessonSlot)
                    && IsDifferentSlotIfNeeded(pupil, lessonSlot);
            }
            else
            {
                return false;
            }
        }

        private bool IsPupilAvaliableInSlot(Pupil pupil, LessonSlotData lessonSlot)
        {
            HashSet<int> avaliableLessonSlots = _pupilAvailabilities[pupil.Id];
            return avaliableLessonSlots.Contains(lessonSlot.Id);
        }

        private static bool IsLongEnoughLessonSlot(Pupil pupil, LessonSlotData lessonSlot)
        {
            return pupil.LessonDuration <= lessonSlot.Duration;
        }

        private bool IsDifferentSlotIfNeeded(Pupil pupil, LessonSlotData lessonSlot)
        {
            if (pupil.NeedsDifferentTimes && _prevTimetable.TryGetValue(pupil.Id, out LessonData? prevLesson))
            {
                return !IsOverlapping(lessonSlot, prevLesson);
            }
            else
            {
                return true;
            }
        }

        private bool IsOverlapping(LessonSlotData lessonSlot, LessonData prevLesson)
        {
            bool isOverlapping = lessonSlot.DayOfWeek == prevLesson.Date.DayOfWeek
                && ((lessonSlot.StartTime <= prevLesson.StartTime && prevLesson.StartTime < lessonSlot.EndTime)
                || prevLesson.StartTime <= lessonSlot.StartTime && lessonSlot.StartTime < prevLesson.EndTime);
            return isOverlapping;
        }

        #endregion
    }
}