using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Converters
{
    public class DictionaryToListConverter<TKey, TValue> : JsonConverter<List<TValue>>
    {
        public override List<TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<TValue> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
