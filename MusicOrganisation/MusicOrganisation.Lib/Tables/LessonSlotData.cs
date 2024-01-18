using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Enums;
using SQLite;

namespace MusicOrganisation.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class LessonSlotData : ITable, IEquatable<LessonSlotData>
    {
        private const string _TABLE_NAME = nameof(LessonSlotData);

        private int _id;
        private DayOfWeek _dayOfWeek;
        private int _flagIndex;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private bool _isDeleted = false;

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
        public TimeSpan StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        [NotNull]
        public TimeSpan EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        [NotNull]
        public bool IsDeleted
        {
            get => _isDeleted;
            set => _isDeleted = value;
        }

        [Ignore]
        public TimeSpan Duration => _endTime - _startTime;

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(DayOfWeek),
                nameof(FlagIndex),
                nameof(StartTime),
                nameof(EndTime),
                nameof(IsDeleted)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(DayOfWeek), _dayOfWeek),
                (nameof(FlagIndex), _flagIndex),
                (nameof(StartTime), _startTime),
                (nameof(EndTime), _endTime),
                (nameof(IsDeleted), _isDeleted)
                );
            return sqlValues;
        }

        public bool Equals(LessonSlotData? other)
        {
            return other != null
                && _dayOfWeek == other._dayOfWeek
                && _flagIndex == other._flagIndex
                && _startTime == other._startTime
                && _endTime == other._endTime
                && _isDeleted == other._isDeleted;
        }
    }
}