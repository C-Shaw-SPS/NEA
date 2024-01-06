using MusicOrganisation.Lib.Databases;
using MusicOrganisation.Lib.Enums;
using SQLite;

namespace MusicOrganisation.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class RepertoireData : ITable, IEquatable<RepertoireData>
    {
        private const string _TABLE_NAME = nameof(RepertoireData);

        private int _id;
        private int _pupilId;
        private int _workId;
        private DateTime? _dateStarted;
        private string _syllabus = string.Empty;
        private RepertoireStatus _status;

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
        public int WorkId
        {
            get => _workId;
            set => _workId = value;
        }

        public DateTime? DateStarted
        {
            get => _dateStarted;
            set => _dateStarted = value;
        }

        [NotNull]
        public string Syllabus
        {
            get => _syllabus;
            set => _syllabus = value;
        }

        [NotNull]
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
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _workId,
                _dateStarted,
                _syllabus,
                _status);
        }

        public bool Equals(RepertoireData? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _workId == other._workId
                && _dateStarted == other._dateStarted
                && _syllabus == other._syllabus
                && _status == other._status;
        }
    }
}
