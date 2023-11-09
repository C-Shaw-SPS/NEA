using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;

namespace MusicOrganisationTests.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class PupilData : IEquatable<PupilData>, ITable
    {
        private const string _TABLE_NAME = nameof(PupilData);

        private int _id;
        private string _name = string.Empty;
        private string _level = string.Empty;
        private Day _lessonDays;
        private bool _hasDifferentTimes;
        private string? _email;
        private string? _phoneNumber;

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
        public Day LessonDays
        {
            get => _lessonDays;
            set => _lessonDays = value;
        }

        [NotNull]
        public bool HasDifferentTimes
        {
            get => _hasDifferentTimes;
            set => _hasDifferentTimes = value;
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

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name),
                nameof(Level),
                nameof(LessonDays),
                nameof(HasDifferentTimes),
                nameof(Email),
                nameof(PhoneNumber)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _name,
                _level,
                _lessonDays,
                _hasDifferentTimes,
                _email,
                _phoneNumber);
        }

        public bool Equals(PupilData? other)
        {
            return other != null
                && _name == other._name
                && _level == other._level
                && _lessonDays == other._lessonDays
                && _hasDifferentTimes == other._hasDifferentTimes
                && _email == other._email
                && _phoneNumber == other._phoneNumber;
        }
    }
}