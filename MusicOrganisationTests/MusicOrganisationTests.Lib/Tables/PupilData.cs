using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;
using SQLitePCL;

namespace MusicOrganisationTests.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class PupilData : IEquatable<PupilData>, ITable
    {
        private const string _TABLE_NAME = nameof(PupilData);

        private int _id;
        private string _name = string.Empty;
        private string _level = string.Empty;
        private bool _needsDifferentTimes;
        private TimeSpan _lessonDuration;
        private int _mondayLessonSlots;
        private int _tuesdayLessonSlots;
        private int _wednesdayLessonSlots;
        private int _thursdayLessonSlots;
        private int _fridayLessonSlots;
        private int _saturdayLessonSlots;
        private int _sundayLessonSlots;
        private string? _email;
        private string? _phoneNumber;
        private string? _notesFile;

        public static string TableName => _TABLE_NAME;

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
            get => _mondayLessonSlots;
            set => _mondayLessonSlots = value;
        }

        [NotNull]
        public int TuesdayLessonSlots
        {
            get => _tuesdayLessonSlots;
            set => _tuesdayLessonSlots = value;
        }

        [NotNull]
        public int WednesdayLessonSlots
        {
            get => _wednesdayLessonSlots;
            set => _wednesdayLessonSlots = value;
        }

        [NotNull]
        public int ThursdayLessonSlots
        {
            get => _thursdayLessonSlots;
            set => _thursdayLessonSlots = value;
        }

        [NotNull]
        public int FridayLessonSlots
        {
            get => _fridayLessonSlots;
            set => _fridayLessonSlots = value;
        }

        [NotNull]
        public int SaturdayLessonSlots
        {
            get => _saturdayLessonSlots;
            set => _saturdayLessonSlots = value;
        }

        [NotNull]
        public int SundayLessonSlots
        {
            get => _sundayLessonSlots;
            set => _sundayLessonSlots = value;
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
                nameof(NotesFile)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _name,
                _level,
                _needsDifferentTimes,
                _lessonDuration,
                _mondayLessonSlots,
                _tuesdayLessonSlots,
                _wednesdayLessonSlots,
                _thursdayLessonSlots,
                _fridayLessonSlots,
                _saturdayLessonSlots,
                _sundayLessonSlots,
                _email,
                _phoneNumber,
                _notesFile);
        }

        public bool Equals(PupilData? other)
        {
            return other != null
                && _name == other._name
                && _level == other._level
                && _needsDifferentTimes == other._needsDifferentTimes
                && _lessonDuration == other._lessonDuration
                && _mondayLessonSlots == other._mondayLessonSlots
                && _tuesdayLessonSlots == other._tuesdayLessonSlots
                && _wednesdayLessonSlots == other._wednesdayLessonSlots
                && _thursdayLessonSlots == other._thursdayLessonSlots
                && _fridayLessonSlots == other._fridayLessonSlots
                && _saturdayLessonSlots == other._saturdayLessonSlots
                && _sundayLessonSlots == other._sundayLessonSlots
                && _email == other._email
                && _phoneNumber == other._phoneNumber
                && _notesFile == other._notesFile;
        }
    }
}