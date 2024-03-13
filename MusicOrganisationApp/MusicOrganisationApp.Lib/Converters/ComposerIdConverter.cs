using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Converters
{
    internal class ComposerIdConverter : JsonConverter<int>
    {
        private const string _ID = "id";

        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            AssertCorrectType(typeToConvert);
            CheckStartObject(ref reader);
            return GetComposerId(ref reader);
        }

        private static void AssertCorrectType(Type typeToConvert)
        {
            if (typeToConvert != typeof(int))
            {
                throw new JsonException("Invalid type");
            }
        }

        private static void CheckStartObject(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
        }

        private static int GetComposerId(ref Utf8JsonReader reader)
        {
            int? id = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return GetIfNotNull(id);
                }

                string? propertyName = reader.GetString();
                if (propertyName == _ID)
                {
                    id = ParseJsonIdProperty(ref reader);
                }
            }

            throw new JsonException();
        }

        private static int GetIfNotNull(int? value)
        {
            if (value is not null)
            {
                return (int)value;
            }
            else
            {
                throw new Exception("value was null");
            }
        }

        private static int ParseJsonIdProperty(ref Utf8JsonReader reader)
        {
            reader.Read();
            string? idString = reader.GetString();
            if (idString is not null)
            {
                return int.Parse(idString);
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