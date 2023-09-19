using OpenOpusDatabase.Lib.Converters;
using SQLite;
using System.Text.Json.Serialization;

namespace OpenOpusDatabase.Lib.Models
{
    [Table("Works")]
    public class Work : IIdentifiable, IEquatable<Work>
    {
        private int _id;
        private int _composerId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private string _genre = string.Empty;

        [PrimaryKey, AutoIncrement, JsonPropertyName("id"), JsonConverter(typeof(StringToIntConverter))]
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

        public bool Equals(Work? other)
        {
            return other != null
                && _composerId == other._composerId
                && _title == other._title
                && _subtitle == other._subtitle
                && _genre == other._genre;
        }
    }
}