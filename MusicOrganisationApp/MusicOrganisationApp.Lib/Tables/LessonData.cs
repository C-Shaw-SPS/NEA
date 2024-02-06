using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class LessonData : ITable, IEquatable<LessonData>, IPupilIdentifiable
    {
        private const string _TABLE_NAME = nameof(LessonData);

        private int _id;
        private int _pupilId;
        private DateTime _date;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private string _notes = string.Empty;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull]
        public int PupilId
        {
            get => _pupilId;
            set => _pupilId = value;
        }

        [NotNull]
        public DateTime Date
        {
            get => _date;
            set => _date = value.Date;
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
        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(Date),
                nameof(StartTime),
                nameof(EndTime),
                nameof(Notes)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(PupilId), _pupilId),
                (nameof(Date), _date),
                (nameof(StartTime), _startTime),
                (nameof(EndTime), _endTime),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }

        public bool Equals(LessonData? other)
        {
            return other != null
                && _id == other._id
                && _pupilId == other._pupilId
                && _date == other._date
                && _startTime == other._startTime
                && _endTime == other._endTime
                && _notes == other._notes;
        }
    }
}