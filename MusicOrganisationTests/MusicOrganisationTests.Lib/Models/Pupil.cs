using MusicOrganisationTests.Lib.Databases;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Pupil : ISqlStorable
    {
        private const string _TABLE_NAME = "Pupils";

        private int _id;
        private string _name = string.Empty;
        private string _level = string.Empty;
        private TimeSpan _lessonDuration;

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

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name),
                nameof(Level),
                nameof(LessonDuration)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            List<object?> values = new()
            {
                _id,
                _name,
                _level,
                _lessonDuration
            };
            return values.FormatAsSqlValues();
        }
    }
}