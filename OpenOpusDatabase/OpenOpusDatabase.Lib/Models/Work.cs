using OpenOpusDatabase.Lib.Converters;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Models
{
    [Table("Works")]
    public class Work : IEquatable<Work>, IIdentifiable
    {
        private int _id;
        private int _composerId;
        private string _title;
        private string _subtitle;
        private string _genre;

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

        public bool Equals(Work other)
        {
            return other != null
                && _id == other._id
                && _composerId == other._composerId
                && _title == other._title
                && _subtitle == other._subtitle
                && _genre == other._genre;
        }
    }
}