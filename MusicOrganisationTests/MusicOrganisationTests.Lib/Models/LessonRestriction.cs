using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class LessonRestriction : ISqlStorable, IEquatable<LessonRestriction>
    {
        private const string _TABLE_NAME = "LessonRestrictions";

        private int _id;
        private int _pupilId;
        private Day _day;
        private DateTime _startTime;
        private DateTime _endTime;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        public Day Day
        {
            get => _day;
            set => _day = value;
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
                nameof(PupilId),
                nameof(Day),
                nameof(StartTime),
                nameof(EndTime)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _day,
                _startTime,
                _endTime);
        }

        public bool Equals(LessonRestriction? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _day == other._day
                && _startTime == other._startTime
                && _endTime == other._endTime;
        }
    }
}