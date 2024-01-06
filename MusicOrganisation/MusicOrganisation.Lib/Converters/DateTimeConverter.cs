using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicOrganisation.Lib.Converters
{
    internal class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(DateTime))
            {
                return GetDateTime(ref reader);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static DateTime GetDateTime(ref Utf8JsonReader reader)
        {
            string? s = reader.GetString();
            if (s != null)
            {
                return DateTime.Parse(s);
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}