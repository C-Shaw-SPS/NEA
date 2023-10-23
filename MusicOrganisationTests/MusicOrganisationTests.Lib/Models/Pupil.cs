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
        private TimeSpan _lessonDuration;
        private Day _lessonDay;
        private bool _differentTimes;
        private string? _email;
        private string? _phoneNumber;

        public static string TableName => _TABLE_NAME;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        public TimeSpan LessonDuration
        {
            get
            {
                return _lessonDuration;
            }
            set
            {
                _lessonDuration = value;
            }
        }

        public Day LessonDay
        {
            get
            {
                return _lessonDay;
            }
            set
            {
                _lessonDay = value;
            }
        }

        public bool DifferentTimes
        {
            get
            {
                return _differentTimes;
            }
            set
            {
                _differentTimes = value;
            }
        }

        public string? Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string? PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }

        public bool Equals(Pupil? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _level == other._level
                && _lessonDay == other._lessonDay
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
                nameof(LessonDuration),
                nameof(LessonDay),
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
                _lessonDuration,
                _lessonDay,
                _differentTimes,
                _email,
                _phoneNumber);
        }
    }
}