using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Converters
{
    public class DictionaryToListConverter<TKey, TValue> : JsonConverter<List<TValue>>
    {
        public override List<TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            List<TValue> values = new();

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
                    TValue? value = JsonSerializer.Deserialize<TValue>(document);

                    if (value != null)
                    {
                        values.Add(value);
                    }
                }

            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, List<TValue> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
