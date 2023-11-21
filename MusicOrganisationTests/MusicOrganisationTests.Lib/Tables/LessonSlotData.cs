using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;

namespace MusicOrganisationTests.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class LessonSlotData : ITable, IEquatable<LessonSlotData>
    {
        private const string _TABLE_NAME = nameof(LessonSlotData);

        private int _id;
        private DayOfWeek _dayOfWeek;
        private int _flagIndex;
        private DateTime _startTime;
        private DateTime _endTime;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull]
        public DayOfWeek DayOfWeek
        {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }

        [NotNull]
        public int FlagIndex
        {
            get => _flagIndex;
            set => _flagIndex = value;
        }

        [NotNull]
        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        [NotNull]
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
                nameof(FlagIndex),
                nameof(StartTime),
                nameof(EndTime)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _dayOfWeek,
                _flagIndex,
                _startTime,
                _endTime);
        }

        public bool Equals(LessonSlotData? other)
        {
            return other != null
                && _dayOfWeek == other._dayOfWeek
                && _flagIndex == other._flagIndex
                && _startTime == other._startTime
                && _endTime == other._endTime;
        }
    }
}