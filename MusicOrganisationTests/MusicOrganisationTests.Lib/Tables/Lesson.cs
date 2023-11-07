using MusicOrganisationTests.Lib.Databases;
using SQLite;

namespace MusicOrganisationTests.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class Lesson : ITable, IEquatable<Lesson>
    {
        private const string _TABLE_NAME = "Lessons";

        private int _id;
        private int _pupilId;
        private int _lessonTimeId;
        private DateTime _date;
        private string? _notesFile;

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
        public int LessonTimeId
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

        public string? NotesFile
        {
            get => _notesFile;
            set => _notesFile = value;
        }


        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(LessonTimeId),
                nameof(Date),
                nameof(NotesFile)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _lessonTimeId,
                _date,
                _notesFile);
        }

        public bool Equals(Lesson? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _lessonTimeId == other._lessonTimeId
                && _date == other._date
                && _notesFile == other._notesFile;
        }
    }
}