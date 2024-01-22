using SQLite;
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Exceptions;

namespace MusicOrganisationApp.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Pupil : ITable, IEquatable<Pupil>
    {
        private const string _TABLE_NAME = $"{nameof(Pupil)}s";

        private int _id;
        private string _name = string.Empty;
        private string _level = string.Empty;
        private bool _needsDifferentTimes;
        private TimeSpan _lessonDuration;
        private readonly Dictionary<DayOfWeek, int> _lessonSlots = GetEmptyLessonSlots();
        private string _email = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _notes = string.Empty;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [NotNull]
        public string Level
        {
            get => _level;
            set => _level = value;
        }

        [NotNull]
        public bool NeedsDifferentTimes
        {
            get => _needsDifferentTimes;
            set => _needsDifferentTimes = value;
        }

        [NotNull]
        public TimeSpan LessonDuration
        {
            get => _lessonDuration;
            set => _lessonDuration = value;
        }

        [NotNull]
        public int MondayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Monday];
            set => _lessonSlots[DayOfWeek.Monday] = value;
        }

        [NotNull]
        public int TuesdayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Tuesday];
            set => _lessonSlots[DayOfWeek.Tuesday] = value;
        }

        [NotNull]
        public int WednesdayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Wednesday];
            set => _lessonSlots[DayOfWeek.Wednesday] = value;
        }

        [NotNull]
        public int ThursdayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Thursday];
            set => _lessonSlots[DayOfWeek.Thursday] = value;
        }

        [NotNull]
        public int FridayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Friday];
            set => _lessonSlots[DayOfWeek.Friday] = value;
        }

        [NotNull]
        public int SaturdayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Saturday];
            set => _lessonSlots[DayOfWeek.Saturday] = value;
        }

        [NotNull]
        public int SundayLessonSlots
        {
            get => _lessonSlots[DayOfWeek.Sunday];
            set => _lessonSlots[DayOfWeek.Sunday] = value;
        }

        [NotNull]
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        [NotNull]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        [NotNull]
        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public static string TableName => _TABLE_NAME;

        private static Dictionary<DayOfWeek, int> GetEmptyLessonSlots()
        {
            Dictionary<DayOfWeek, int> dictionary = [];
            for (DayOfWeek day = DayOfWeek.Sunday; day <= DayOfWeek.Saturday; ++day)
            {
                dictionary.Add(day, 0);
            }
            return dictionary;
        }

        public int GetTotalLessonSlots()
        {
            int totalSlots = 0;
            foreach (int flags in _lessonSlots.Values)
            {
                totalSlots += flags.GetNumberOfFlags();
            }
            return totalSlots;
        }

        public bool HasAnyLessonSlots()
        {
            foreach (int flags in _lessonSlots.Values)
            {
                if (flags != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasFixedLessonSlot()
        {
            return GetTotalLessonSlots() == 1;
        }

        public bool HasVariableLessonSlots()
        {
            return GetTotalLessonSlots() > 1;
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
                && _email == other._email
                && _phoneNumber == other._phoneNumber
                && _notes == other._notes;
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

        public bool IsAvaliableInSlot(LessonSlotData lessonSlot)
        {
            return _lessonSlots[lessonSlot.DayOfWeek].HasFlagAtIndex(lessonSlot.FlagIndex);
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

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name),
                nameof(Level),
                nameof(NeedsDifferentTimes),
                nameof(LessonDuration),
                nameof(MondayLessonSlots),
                nameof(TuesdayLessonSlots),
                nameof(WednesdayLessonSlots),
                nameof(ThursdayLessonSlots),
                nameof(FridayLessonSlots),
                nameof(SaturdayLessonSlots),
                nameof(SundayLessonSlots),
                nameof(Email),
                nameof(PhoneNumber),
                nameof(Notes)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(Name), _name),
                (nameof(Level), _level),
                (nameof(NeedsDifferentTimes), _needsDifferentTimes),
                (nameof(LessonDuration), _lessonDuration),
                (nameof(MondayLessonSlots), _lessonSlots[DayOfWeek.Monday]),
                (nameof(TuesdayLessonSlots), _lessonSlots[DayOfWeek.Tuesday]),
                (nameof(WednesdayLessonSlots), _lessonSlots[DayOfWeek.Wednesday]),
                (nameof(ThursdayLessonSlots), _lessonSlots[DayOfWeek.Thursday]),
                (nameof(FridayLessonSlots), _lessonSlots[DayOfWeek.Friday]),
                (nameof(SaturdayLessonSlots), _lessonSlots[DayOfWeek.Saturday]),
                (nameof(SundayLessonSlots), _lessonSlots[DayOfWeek.Sunday]),
                (nameof(Email), _email),
                (nameof(PhoneNumber), _phoneNumber),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }
    }
}