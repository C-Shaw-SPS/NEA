using MusicOrganisationTests.Lib.Databases;
using MusicOrganisationTests.Lib.Enums;
using SQLite;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Repertoire : ISqlStorable, IEquatable<Repertoire>
    {
        private const string _TABLE_NAME = "Repertoire";

        private int _id;
        private int _pupilId;
        private int _workId;
        private DateTime _dateStarted;
        private string _syllabus = string.Empty;
        private RepertoireStatus _status;

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

        public int WorkId
        {
            get => _workId;
            set => _workId = value;
        }

        public DateTime DateStarted
        {
            get => _dateStarted;
            set => _dateStarted = value;
        }

        public string Syllabus
        {
            get => _syllabus;
            set => _syllabus = value;
        }

        public RepertoireStatus Status
        {
            get => _status;
            set => _status = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(PupilId),
                nameof(WorkId),
                nameof(DateStarted),
                nameof(Syllabus),
                nameof(Status)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _pupilId,
                _workId,
                _dateStarted,
                _syllabus,
                _status);
        }

        public bool Equals(Repertoire? other)
        {
            return other != null
                && _id == other._id
                && _pupilId == other._pupilId
                && _workId == other._workId
                && _dateStarted == other._dateStarted
                && _syllabus == other._syllabus
                && _status == other._status;
        }
    }
}
