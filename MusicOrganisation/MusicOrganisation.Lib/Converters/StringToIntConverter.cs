using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicOrganisation.Lib.Converters
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
                throw new NotSupportedException();
            }
        }

        private static int GetInt(Utf8JsonReader reader)
        {
            string? s = reader.GetString();
            if (s != null)
            {
                return int.Parse(s);
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