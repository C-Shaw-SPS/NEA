﻿using MusicOrganisationApp.Lib.Converters;
using MusicOrganisationApp.Lib.Databases;
using SQLite;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class ComposerData : IEquatable<ComposerData>, ITable
    {
        private const string _TABLE_NAME = nameof(ComposerData);

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

        [JsonPropertyName("birth"), JsonConverter(typeof(YearConverter))]
        public int? BirthYear
        {
            get => _birthYear;
            set => _birthYear = value;
        }

        [JsonPropertyName("death"), JsonConverter(typeof(YearConverter))]
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

        public static IEnumerable<string> GetColumnNames()
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

        public bool Equals(ComposerData? other)
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