using MusicOrganisationApp.Lib.Databases;
using SQLite;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class CaregiverData : ITable, IEquatable<CaregiverData>, IContactablePerson
    {
        private const string _TABLE_NAME = nameof(CaregiverData);

        private int _id;
        private string _name = string.Empty;
        private string _emailAddress = string.Empty;
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
        public string EmailAddress
        {
            get => _emailAddress;
            set => _emailAddress = value;
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
                nameof(EmailAddress),
                nameof(PhoneNumber)
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(Name), _name),
                (nameof(EmailAddress), _emailAddress),
                (nameof(PhoneNumber), _phoneNumber)
                );
            return sqlValues;
        }

        public bool Equals(CaregiverData? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _emailAddress == other._emailAddress
                && _phoneNumber == other._phoneNumber;
        }
    }
}