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

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            int? id = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    if (id != null)
                    {
                        return (int)id;
                    }
                    else
                    {
                        throw new JsonException();
                    }
                }

                string? propertyName = reader.GetString();
                if (propertyName == "id")
                {
                    reader.Read();
                    string? idString = reader.GetString();
                    if (idString != null)
                    {
                        id = int.Parse(idString);
                    }
                    else
                    {
                        throw new JsonException();
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
