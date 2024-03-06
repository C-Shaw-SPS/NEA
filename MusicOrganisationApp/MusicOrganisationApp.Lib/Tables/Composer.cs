using MusicOrganisationApp.Lib.Converters;
using MusicOrganisationApp.Lib.Databases;
using SQLite;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class Composer : IEquatable<Composer>, ITable, IPerson
    {
        private const string _TABLE_NAME = nameof(Composer);

        private int _id;
        private string _name = string.Empty;
        private int? _birthYear;
        private int? _deathYear;
        private string _era = string.Empty;

        [PrimaryKey, JsonPropertyName("id"), JsonConverter(typeof(StringToIntConverter))]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [JsonPropertyName("complete_name"), NotNull]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [JsonPropertyName("birth"), JsonConverter(typeof(DateToYearConverter))]
        public int? BirthYear
        {
            get => _birthYear;
            set => _birthYear = value;
        }

        [JsonPropertyName("death"), JsonConverter(typeof(DateToYearConverter))]
        public int? DeathYear
        {
            get => _deathYear;
            set => _deathYear = value;
        }

        [JsonPropertyName("epoch"), NotNull]
        public string Era
        {
            get => _era;
            set => _era = value;
        }

        public static string TableName => _TABLE_NAME;

        public static IEnumerable<string> GetFieldNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name),
                nameof(BirthYear),
                nameof(DeathYear),
                nameof(Era),
            };
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(Name), _name),
                (nameof(BirthYear), _birthYear),
                (nameof(DeathYear), _deathYear),
                (nameof(Era), _era)
                );
            return sqlValues;
        }

        public bool Equals(Composer? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _birthYear == other._birthYear
                && _deathYear == other._deathYear
                && _era == other._era;
        }
    }
}