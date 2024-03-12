using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Services
{
    public class TimetableGenerator
    {
        private static readonly Random _random = new();

        private Dictionary<int, int> _timetable;
        private Stack<int> _stack;
        private readonly Dictionary<int, LessonSlot> _lessonSlots;
        private readonly Dictionary<int, Pupil> _pupils;
        private readonly Dictionary<int, HashSet<int>> _pupilAvailabilities;
        private readonly List<int> _shuffledPupilIds;
        private readonly Dictionary<int, LessonData> _prevTimetable;
        private readonly int _maxLessonSlotId;

        public TimetableGenerator(IEnumerable<Pupil> pupils, IEnumerable<PupilAvailability> pupilAvailabilities, IEnumerable<LessonSlot> lessonSlots, IEnumerable<LessonData> prevLessons)
        {
            _timetable = [];
            _stack = [];
            _lessonSlots = lessonSlots.GetDictionary();
            _pupils = pupils.GetDictionary();
            _pupilAvailabilities = GetPupilAvailabilities(pupilAvailabilities);
            _shuffledPupilIds = GetShuffledIds(pupils);
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

        private static List<int> GetShuffledIds(IEnumerable<IIdentifiable> values)
        {
            List<int> ids = [];
            foreach (IIdentifiable value in values)
            {
                ids.Add(value.Id);
            }
            Shuffle(ids);
            return ids;
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
            bool succeeded = true;
            succeeded &= TryInsertPupils();
            timetable = new(_timetable);
            return succeeded;
        }

        private bool TryInsertPupils()
        {
            bool succeeded = true;
            foreach (int pupilId in _shuffledPupilIds)
            {
                succeeded &= TryInsertNewPupil(pupilId);
            }
            return succeeded;
        }

        private bool TryInsertNewPupil(int pupilId)
        {
            if (HasAnyAvailability(pupilId))
            {
                Dictionary<int, int> currentTimetable = new(_timetable);
                Stack<int> currentStack = new(_stack);
                bool succeeded = TryInsertPupil(pupilId, 0);
                if (!succeeded)
                {
                    _timetable = currentTimetable;
                    _stack = currentStack;
                }
                return succeeded;
            }
            else
            {
                return false;
            }
        }

        private bool HasAnyAvailability(int pupilId)
        {
            return _pupilAvailabilities[pupilId].Count > 0;
        }

        private bool TryInsertPupil(int pupilId, int minLessonSlotId)
        {
            bool succeeded = TryInsertLesson(pupilId, minLessonSlotId);
            if (succeeded)
            {
                return true;
            }

            while (TryMoveState())
            {
                succeeded = TryInsertLesson(pupilId, 0);
                if (succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        private bool TryInsertLesson(int pupilId, int minLessonSlotId)
        {
            bool canInsert = TryGetValidLessonSlot(pupilId, minLessonSlotId, out int lessonSlotId);

            if (canInsert)
            {
                _timetable.Add(lessonSlotId, pupilId);
                _stack.Push(lessonSlotId);
            }

            return canInsert;
        }

        private bool TryMoveState()
        {
            if (_stack.Count == 0)
            {
                return false;
            }

            int lessonSlotId = _stack.Pop();
            int pupilId = _timetable[lessonSlotId];
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
            if (_lessonSlots.TryGetValue(lessonSlotId, out LessonSlot? lessonSlot))
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

        private bool IsPupilAvaliableInSlot(Pupil pupil, LessonSlot lessonSlot)
        {
            HashSet<int> avaliableLessonSlots = _pupilAvailabilities[pupil.Id];
            return avaliableLessonSlots.Contains(lessonSlot.Id);
        }

        private static bool IsLongEnoughLessonSlot(Pupil pupil, LessonSlot lessonSlot)
        {
            return pupil.LessonDuration <= lessonSlot.Duration;
        }

        private bool IsDifferentSlotIfNeeded(Pupil pupil, LessonSlot lessonSlot)
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

        private bool IsOverlapping(LessonSlot lessonSlot, LessonData prevLesson)
        {
            bool isOverlapping = lessonSlot.DayOfWeek == prevLesson.Date.DayOfWeek
                && ((lessonSlot.StartTime <= prevLesson.StartTime && prevLesson.StartTime < lessonSlot.EndTime)
                || prevLesson.StartTime <= lessonSlot.StartTime && lessonSlot.StartTime < prevLesson.EndTime);
            return isOverlapping;
        }

        #endregion
    }
}