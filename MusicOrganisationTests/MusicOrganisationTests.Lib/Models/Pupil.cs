using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Exceptions;
using MusicOrganisationTests.Lib.Services;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Lib.Models
{
    public class Pupil : IIdentifiable
    {
        private int _id;
        private string _name;
        private string _level;
        private bool _needsDifferentTimes;
        private TimeSpan _lessonDuration;
        private Dictionary<DayOfWeek, int> _lessonSlots;
        private int _totalLessonSlots;
        private string? _email;
        private string? _phoneNumber;
        private string? _notesFile;

        public Pupil(PupilData pupilData)
        {
            _id = pupilData.Id;
            _name = pupilData.Name;
            _level = pupilData.Level;
            _needsDifferentTimes = pupilData.NeedsDifferentTimes;
            _lessonDuration = pupilData.LessonDuration;
            _lessonSlots = GetLessonSlots(pupilData);
            _totalLessonSlots = GetTotalLessonSlots(_lessonSlots);
            _email = pupilData.Email;
            _phoneNumber = pupilData.PhoneNumber;
            _notesFile = pupilData.NotesFile;
        }

        public int Id
        {
            get => _id;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Level
        {
            get => _level;
            set => _level = value;
        }

        public bool NeedsDifferentTimes
        {
            get => _needsDifferentTimes;
            set => _needsDifferentTimes = value;
        }

        public TimeSpan LessonDuration
        {
            get => _lessonDuration;
            set => _lessonDuration = value;
        }

        public int TotalLessonSlots
        {
            get => _totalLessonSlots;
        }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public string? NotesFile
        {
            get => _notesFile;
            set => _notesFile = value;
        }

        public bool HasAnyLessonSlots
        {
            get => _totalLessonSlots != 0;
        }

        public bool HasFixedLessonSlot
        {
            get => _totalLessonSlots == 1;
        }

        private static Dictionary<DayOfWeek, int> GetLessonSlots(PupilData pupilData)
        {
            return new Dictionary<DayOfWeek, int>
            {
                { DayOfWeek.Monday, pupilData.MondayLessonSlots },
                { DayOfWeek.Tuesday, pupilData.TuesdayLessonSlots },
                { DayOfWeek.Wednesday, pupilData.WednesdayLessonSlots },
                { DayOfWeek.Thursday, pupilData.ThursdayLessonSlots },
                { DayOfWeek.Friday, pupilData.FridayLessonSlots },
                { DayOfWeek.Saturday, pupilData.SaturdayLessonSlots },
                { DayOfWeek.Sunday, pupilData.SundayLessonSlots }
            };
        }

        private static int GetTotalLessonSlots(Dictionary<DayOfWeek, int> lessonSlots)
        {
            int totalSlots = 0;
            foreach (int flags in lessonSlots.Values)
            {
                totalSlots += flags.GetNumberOfFlags();
            }
            return totalSlots;
        }

        public bool Equals(Pupil? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _level == other._level
                && _needsDifferentTimes == other._needsDifferentTimes
                && _lessonDuration == other._lessonDuration
                && EqualLessonSlots(_lessonSlots, other._lessonSlots)
                && _totalLessonSlots == other._totalLessonSlots
                && _email == other._email
                && _phoneNumber == other._phoneNumber
                && _notesFile == other._notesFile;
        }

        private static bool EqualLessonSlots(Dictionary<DayOfWeek, int> lessonSlots1, Dictionary<DayOfWeek, int> lessonSlots2)
        {
            for (DayOfWeek day = DayOfWeek.Monday; day <= DayOfWeek.Sunday; ++day)
            {
                if (lessonSlots1[day] != lessonSlots2[day])
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsAvaliableInSlot(DayOfWeek dayOfWeek, int index)
        {
            return _lessonSlots[dayOfWeek].HasFlagAtIndex(index);
        }

        public (DayOfWeek dayOfWeek, int index) GetFixedLessonSlot()
        {
            foreach (DayOfWeek dayOfWeek in _lessonSlots.Keys)
            {
                if (_lessonSlots[dayOfWeek].GetNumberOfFlags() == 1)
                {
                    return (dayOfWeek, _lessonSlots[dayOfWeek].GetIndexOfFirstFlag());
                }
            }

            throw new NoFixedLessonException(this);
        }

        public void AddLessonSlot(LessonSlotData lessonSlot)
        {
            int newFlags = _lessonSlots[lessonSlot.DayOfWeek];
            newFlags.AddFlagAtIndex(lessonSlot.FlagIndex);
            _lessonSlots[lessonSlot.DayOfWeek] = newFlags;
        }

        public void RemoveLessonSlot(LessonSlotData lessonSlot)
        {
            int newFlags = _lessonSlots[lessonSlot.DayOfWeek];
            newFlags.RemoveFlagAtIndex(lessonSlot.FlagIndex);
            _lessonSlots[lessonSlot.DayOfWeek] = newFlags;
        }
    }
}