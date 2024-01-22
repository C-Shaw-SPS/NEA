using MusicOrganisationApp.Lib.Databases;
using MusicOrganisationApp.Lib.Enums;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
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
                nameof(WorkId),
                nameof(DateStarted),
                nameof(Syllabus),
                nameof(Status),
                nameof(Notes)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(PupilId), _pupilId),
                (nameof(WorkId), _workId),
                (nameof(DateStarted), _dateStarted),
                (nameof(Syllabus), _syllabus),
                (nameof(Status), _status),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }

        public bool Equals(RepertoireData? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _workId == other._workId
                && _dateStarted == other._dateStarted
                && _syllabus == other._syllabus
                && _status == other._status
                && _notes == other._notes;
        }
    }
}