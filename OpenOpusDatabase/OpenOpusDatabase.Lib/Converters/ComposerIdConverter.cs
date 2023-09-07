using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Converters
{
    public class ComposerIdConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(int))
            {
                return GetId(reader, options);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static int GetId(Utf8JsonReader reader, JsonSerializerOptions options)
        {
            Composer? composer;
            using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                composer = JsonSerializer.Deserialize<Composer>(jsonDocument, options);
            }
            if (composer != null)
            {
                return composer.Id;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
