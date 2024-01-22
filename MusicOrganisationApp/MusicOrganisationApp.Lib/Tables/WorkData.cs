using MusicOrganisationApp.Lib.Converters;
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

        public IDictionary<string, string> GetSqlValues()
        {
            IDictionary<string, string> sqlValues = SqlFormatting.FormatValues(
                (nameof(Id), _id),
                (nameof(ComposerId), _composerId),
                (nameof(Title), _title),
                (nameof(Subtitle), _subtitle),
                (nameof(Genre), _genre)
                );
            return sqlValues;
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

        public bool Equals(WorkData? other)
        {
            return other != null
                && _composerId == other._composerId
                && _title == other._title
                && _subtitle == other._subtitle
                && _genre == other._genre;
        }
    }
}