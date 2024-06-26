﻿using OpenOpusDatabase.Lib.Converters;
using OpenOpusDatabase.Lib.Databases;
using SQLite;
using System.Text;
using System.Text.Json.Serialization;

namespace OpenOpusDatabase.Lib.Models
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
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [JsonPropertyName("complete_name")]
        public string CompleteName
        {
            get
            {
                return _completeName;
            }
            set
            {
                _completeName = value;
            }
        }

        [JsonPropertyName("birth"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
            }
        }

        [JsonPropertyName("death"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? DeathDate
        {
            get
            {
                return _deathDate;
            }
            set
            {
                _deathDate = value;
            }
        }

        [JsonPropertyName("epoch")]
        public string Era
        {
            get
            {
                return _era;
            }
            set
            {
                _era = value;
            }
        }

        [JsonPropertyName("portrait")]
        public string? PortraitLink
        {
            get
            {
                return _portraitLink;
            }
            set
            {
                if (value != null)
                {
                    _portraitLink = FormatLink(value);
                }
            }
        }

        public static string TableName => _TABLE_NAME;

        private static string FormatLink(string value)
        {
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

        public bool Equals(Composer? other)
        {
            return other != null
                && _id == other._id
                && _name == other._name
                && _completeName == other._completeName
                && _birthDate == other._birthDate
                && _deathDate == other._deathDate
                && _era == other._era
                && _portraitLink == other._portraitLink;
        }

        public IEnumerable<string> GetSqlValues()
        {
            List<object?> values = new()
            {
                _id,
                _name,
                _completeName,
                _birthDate,
                _deathDate,
                _era,
                _portraitLink,
            };
            return values.FormatAsSqlValues();
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
    }
}