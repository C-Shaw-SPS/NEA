using MusicOrganisation.Lib.Databases;
using SQLite;

namespace MusicOrganisation.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class LessonData : ITable, IEquatable<LessonData>
    {
        private const string _TABLE_NAME = nameof(LessonData);

        private int _id;
        private int _pupilId;
        private int _lessonTimeId;
        private DateTime _date;
        private string _notes = string.Empty;
        private bool _isDeleted = false;

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
            get => _lessonTimeId;
            set => _lessonTimeId = value;
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

        [NotNull]
        public bool IsDeleted
        {
            get => _isDeleted;
            set => _isDeleted = value;
        }


        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(LessonSlotId),
                nameof(Date),
                nameof(Notes),
                nameof(IsDeleted)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _lessonTimeId,
                _date,
                _notes,
                _isDeleted);
        }

        public bool Equals(LessonData? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _lessonTimeId == other._lessonTimeId
                && _date == other._date
                && _notes == other._notes
                && _isDeleted == other._isDeleted;
        }
    }
}