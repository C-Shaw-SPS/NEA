using System.Text.Json.Serialization;
using System.Text.Json;

namespace MusicOrganisationApp.Lib.Converters
{
    internal class DictionaryToIEnumerableConverter<TKey, TValue> : JsonConverter<IEnumerable<TValue>>
    {
        public override IEnumerable<TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            List<TValue> values = [];

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return values;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                using (JsonDocument document = JsonDocument.ParseValue(ref reader))
                {
                    TValue? value = document.Deserialize<TValue>();

                    if (value != null)
                    {
                        values.Add(value);
                    }
                }

            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<TValue> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}