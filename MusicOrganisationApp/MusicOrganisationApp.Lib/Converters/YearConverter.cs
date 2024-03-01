using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Converters
{
    internal class YearConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(int?))
            {
                return GetDateTime(ref reader);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static int? GetDateTime(ref Utf8JsonReader reader)
        {
            string? s = reader.GetString();
            if (s is not null)
            {
                return DateTime.Parse(s).Year;
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}