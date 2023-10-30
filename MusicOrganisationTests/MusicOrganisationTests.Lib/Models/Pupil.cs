using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Pupil : IEquatable<Pupil>, ISqlStorable
    {
        private const string _TABLE_NAME = "Pupils";

        private int _id;
        private string _name = string.Empty;
        private string _level = string.Empty;
        private Day _lessonDays;
        private bool _differentTimes;
        private string? _email;
        private string? _phoneNumber;

        public static string TableName => _TABLE_NAME;

        public int Id
        {
            get => _id;
            set => _id = value;
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

        public Day LessonDays
        {
            get => _lessonDays;
            set => _lessonDays = value;
        }

        public bool DifferentTimes
        {
            get => _differentTimes;
            set => _differentTimes = value;
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

        public bool Equals(Pupil? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _level == other._level
                && _lessonDays == other._lessonDays
                && _differentTimes == other._differentTimes
                && _email == other._email
                && _phoneNumber == other._phoneNumber;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name),
                nameof(Level),
                nameof(LessonDays),
                nameof(DifferentTimes),
                nameof(Email),
                nameof(PhoneNumber)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _name,
                _level,
                _lessonDays,
                _differentTimes,
                _email,
                _phoneNumber);
        }
    }
}