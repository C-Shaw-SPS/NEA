using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class LessonData : ITable, IEquatable<LessonData>
    {
        private const string _TABLE_NAME = nameof(LessonData);

        private int _id;
        private int _pupilId;
        private int _lessonSlotId;
        private DateTime _date;
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
        public int LessonSlotId
        {
            get => _lessonSlotId;
            set => _lessonSlotId = value;
        }

        [NotNull]
        public DateTime Date
        {
            get => _date;
            set => _date = value.Date;
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
                nameof(LessonSlotId),
                nameof(Date),
                nameof(Notes)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(PupilId), _pupilId),
                (nameof(LessonSlotId), _lessonSlotId),
                (nameof(Date), _date),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }

        public bool Equals(LessonData? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _lessonSlotId == other._lessonSlotId
                && _date == other._date
                && _notes == other._notes;
        }
    }
}