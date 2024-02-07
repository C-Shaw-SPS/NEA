using SQLite;

/* Unmerged change from project 'MusicOrganisationApp.Lib (net8.0)'
Before:
using MusicOrganisationApp.Lib.Databases;
After:
using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp;
using MusicOrganisationApp.Lib;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Tables;
*/
using MusicOrganisationApp.Lib.Databases;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class Pupil : ITable, IEquatable<Pupil>
    {
        private const string _TABLE_NAME = $"{nameof(Pupil)}s";

        private int _id;
        private string _name = string.Empty;
        private string _level = string.Empty;
        private bool _needsDifferentTimes = false;
        private TimeSpan _lessonDuration;
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

        public bool Equals(Pupil? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _level == other._level
                && _needsDifferentTimes == other._needsDifferentTimes
                && _lessonDuration == other._lessonDuration
                && _email == other._email
                && _phoneNumber == other._phoneNumber
                && _notes == other._notes;
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
                (nameof(Email), _email),
                (nameof(PhoneNumber), _phoneNumber),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }
    }
}