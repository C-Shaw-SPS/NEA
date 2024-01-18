using MusicOrganisation.Lib.Databases;
using SQLite;

namespace MusicOrganisation.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class CaregiverMap : ITable, IEquatable<CaregiverMap>
    {
        private const string _TABLE_NAME = nameof(CaregiverMap);

        private int _id;
        private int _pupilId;
        private int _caregiverId;
        private string _description = string.Empty;
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
        public int CaregiverId
        {
            get => _caregiverId;
            set => _caregiverId = value;
        }

        [NotNull]
        public string Description
        {
            get => _description;
            set => _description = value;
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
                nameof(CaregiverId),
                nameof(Description),
                nameof(IsDeleted)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _pupilId,
                _caregiverId,
                _description,
                _isDeleted);
        }
        public bool Equals(CaregiverMap? other)
        {
            return other != null
                && _pupilId == other._pupilId
                && _caregiverId == other._caregiverId
                && _description == other._description
                && _isDeleted == other._isDeleted;
        }
    }
}