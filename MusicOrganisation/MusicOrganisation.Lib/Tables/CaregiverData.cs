using MusicOrganisation.Lib.Databases;
using SQLite;

namespace MusicOrganisation.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class CaregiverData : ITable, IEquatable<CaregiverData>
    {
        private const string _TABLE_NAME = nameof(CaregiverData);

        private int _id;
        private string _name = string.Empty;
        private string? _email;
        private string? _phoneNumber;

        public static string TableName => _TABLE_NAME;

        [PrimaryKey]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>()
            {
                nameof(Id),
                nameof(Name),
                nameof(Email),
                nameof(PhoneNumber)
            };
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _name,
                _email,
                _phoneNumber);
        }

        public bool Equals(CaregiverData? other)
        {
            return other != null
                && _name == other._name
                && _email == other._email
                && _phoneNumber == other._phoneNumber;
        }
    }
}