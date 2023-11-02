using MusicOrganisationTests.Lib.Converters;
using MusicOrganisationTests.Lib.Databases;
using SQLite;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Composer : IEquatable<Composer>, ISqlStorable
    {
        private const string _TABLE_NAME = "Composers";

        private int _id;
        private string _name = string.Empty;
        private string _completeName = string.Empty;
        private DateTime _birthDate;
        private DateTime? _deathDate;
        private string _era = string.Empty;
        private string? _portraitLink;

        [PrimaryKey, JsonPropertyName("id"), JsonConverter(typeof(StringToIntConverter))]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [JsonPropertyName("name"), NotNull]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [JsonPropertyName("complete_name"), NotNull]
        public string CompleteName
        {
            get => _completeName;
            set => _completeName = value;
        }

        [JsonPropertyName("birth"), JsonConverter(typeof(DateTimeConverter)), NotNull]
        public DateTime BirthDate
        {
            get => _birthDate;
            set => _birthDate = value;
        }

        [JsonPropertyName("death"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DeathDate
        {
            get => _deathDate;
            set => _deathDate = value;
        }

        [JsonPropertyName("epoch"), NotNull]
        public string Era
        {
            get => _era;
            set => _era = value;
        }

        [JsonPropertyName("portrait")]
        public string? PortraitLink
        {
            get => _portraitLink;
            set => _portraitLink = FormatLink(value);
        }

        public static string TableName => _TABLE_NAME;

        private static string? FormatLink(string? value)
        {
            if (value == null)
            {
                return value;
            }

            StringBuilder link = new();
            foreach (char c in value)
            {
                if (c != '\\')
                {
                    link.Append(c);
                }
            }
            return link.ToString();
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatValues(
                _id,
                _name,
                _completeName,
                _birthDate,
                _deathDate,
                _era,
                _portraitLink);
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(Name),
                nameof(CompleteName),
                nameof(BirthDate),
                nameof(DeathDate),
                nameof(Era),
                nameof(PortraitLink)
            };
        }

        public bool Equals(Composer? other)
        {
            return other != null
                && _name == other._name
                && _completeName == other._completeName
                && _birthDate == other._birthDate
                && _deathDate == other._deathDate
                && _era == other._era
                && _portraitLink == other._portraitLink;
        }
    }
}