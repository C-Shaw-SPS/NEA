﻿using MusicOrganisationApp.Lib.Converters;
using MusicOrganisationApp.Lib.Databases;
using SQLite;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Tables
{
    [Table(_TABLE_NAME)]
    public class WorkData : ITable, IEquatable<WorkData>
    {
        private const string _TABLE_NAME = nameof(WorkData);
        private int _id;
        private int _composerId;
        private string _title = string.Empty;
        private string _subtitle = string.Empty;
        private string _genre = string.Empty;
        private string _notes = string.Empty;
        public static string TableName => _TABLE_NAME;


        [PrimaryKey, JsonPropertyName("id"), JsonConverter(typeof(StringToIntConverter))]
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        [NotNull, JsonPropertyName("composer"), JsonConverter(typeof(ComposerIdConverter))]
        public int ComposerId
        {
            get => _composerId;
            set => _composerId = value;
        }

        [NotNull, JsonPropertyName("title")]
        public string Title
        {
            get => _title;
            set => _title = value;
        }

        [NotNull, JsonPropertyName("subtitle")]
        public string Subtitle
        {
            get => _subtitle;
            set => _subtitle = value;
        }

        [NotNull, JsonPropertyName("genre")]
        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        [NotNull]
        public string Notes
        {
            get => _notes;
            set => _notes = value;
        }

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(ComposerId), _composerId),
                (nameof(Title), _title),
                (nameof(Subtitle), _subtitle),
                (nameof(Genre), _genre),
                (nameof(Notes), _notes)
                );
            return sqlValues;
        }

        public static IEnumerable<string> GetFieldNames()
        {
            return new List<string>
            {
                nameof(Id),
                nameof(ComposerId),
                nameof(Title),
                nameof(Subtitle),
                nameof(Genre),
                nameof(Notes)
            };
        }

        public bool Equals(WorkData? other)
        {
            return other is not null
                && _id == other._id
                && _composerId == other._composerId
                && _title == other._title
                && _subtitle == other._subtitle
                && _genre == other._genre
                && _notes == other._notes;
        }
    }
}