using MusicOrganisationTests.Lib.Databases;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Lesson : ISqlStorable, IEquatable<Lesson>
    {
        private const string _TABLE_NAME = "Lessons";

        private int _id;
        private int _pupilId;
        private DateTime _date;
        private string? _notesFile;

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

        public DateTime Date
        {
            get => _date;
            set => _date = value;
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
                nameof(Date),
                nameof(NotesFile)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _pupilId,
                _date,
                _notesFile);
        }

        public bool Equals(Lesson? other)
        {
            return other != null
                && _id == other._id
                && _pupilId == other._pupilId
                && _date == other._date
                && _notesFile == other._notesFile;
        }
    }
}
