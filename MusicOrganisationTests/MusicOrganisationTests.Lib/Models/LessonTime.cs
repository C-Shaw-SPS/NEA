using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    internal class LessonTime : ISqlStorable, IEquatable<LessonTime>
    {
        private const string _TABLE_NAME = "LessonTimes";

        private int _id;
        private Day _dayOfWeek;
        private DateTime _startTime;
        private DateTime _endTime;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public Day DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }

        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(DayOfWeek),
                nameof(StartTime),
                nameof(EndTime)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _dayOfWeek,
                _startTime,
                _endTime);
        }
        
        public bool Equals(LessonTime? other)
        {
            return other != null
                && _id == other._id
                && _dayOfWeek == other._dayOfWeek
                && _startTime == other._startTime
                && _endTime == other._endTime;
        }
    }
}