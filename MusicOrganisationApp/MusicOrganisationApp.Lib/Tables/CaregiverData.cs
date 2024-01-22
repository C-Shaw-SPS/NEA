using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class CaregiverData : ITable, IEquatable<CaregiverData>
    {
        private const string _TABLE_NAME = nameof(CaregiverData);

        private int _id;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _phoneNumber = string.Empty;

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

        [NotNull]
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        [NotNull]
        public string PhoneNumber
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

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(Name), _name),
                (nameof(Email), _email),
                (nameof(PhoneNumber), _phoneNumber)
                );
            return sqlValues;
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