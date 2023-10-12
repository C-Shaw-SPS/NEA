using MusicOrganisationTests.Lib.Converters;
using MusicOrganisationTests.Lib.Databases;
using SQLite;
using System.Text.Json.Serialization;

namespace MusicOrganisationTests.Lib.Models
{
    [Table(_TABLE_NAME)]
    public class Work : ISqlStorable, IEquatable<Work>
    {
        private const string _TABLE_NAME = "Works";
        private int _id;
        private int _composerId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private string _genre = string.Empty;

        [PrimaryKey, JsonPropertyName("id"), JsonConverter(typeof(StringToIntConverter))]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [JsonPropertyName("composer"), JsonConverter(typeof(ComposerIdConverter))]
        public int ComposerId
        {
            get
            {
                return _composerId;
            }
            set
            {
                _composerId = value;
            }
        }

        [JsonPropertyName("title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        [JsonPropertyName("subtitle")]
        public string Subtitle
        {
            get
            {
                return _subtitle;
            }
            set
            {
                _subtitle = value;
            }
        }

        [JsonPropertyName("genre")]
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
            }
        }

        public static string TableName => _TABLE_NAME;

        public bool Equals(Work? other)
        {
            return other != null
                && _id == other._id
                && _composerId == other._composerId
                && _title == other._title
                && _subtitle == other._subtitle
                && _genre == other._genre;
        }

        public IEnumerable<string> GetSqlValues()
        {
            return SqlFormatting.FormatAsSqlValues(
                _id,
                _composerId,
                _title,
                _subtitle,
                _genre);
        }

        public static IEnumerable<string> GetColumnNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(ComposerId),
                nameof(Title),
                nameof(Subtitle),
                nameof(Genre)
            };
        }
    }
}