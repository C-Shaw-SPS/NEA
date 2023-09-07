using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Converters
{
    public class DictionaryToListConverter<TKey, TValue> : JsonConverter<List<TValue>> where TKey : notnull
    {
        public override List<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(List<TValue>))
            {
                return GetValues(reader, options);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static List<TValue> GetValues(Utf8JsonReader reader, JsonSerializerOptions options)
        {
            Dictionary<TKey, TValue>? dictionary;
            using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                dictionary = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(jsonDocument, options);
            }
            if (dictionary != null)
            {
                return dictionary.Values.ToList();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public override void Write(Utf8JsonWriter writer, List<TValue> value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

    }
}