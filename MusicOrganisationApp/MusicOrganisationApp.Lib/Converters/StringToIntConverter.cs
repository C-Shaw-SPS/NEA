using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Converters
{
    internal class StringToIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(int))
            {
                return GetInt(reader);
            }
            else
            {
                throw new JsonException();
            }
        }

        private static int GetInt(Utf8JsonReader reader)
        {
            string? s = reader.GetString();
            if (s is not null && int.TryParse(s, out int n))
            {
                return n;
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}