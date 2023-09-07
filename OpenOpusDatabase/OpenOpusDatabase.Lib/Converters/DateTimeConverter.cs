﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(DateTime))
            {
                return GetDateTime(reader);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private DateTime GetDateTime(Utf8JsonReader reader)
        {
            string? s = reader.GetString();
            if (s != null)
            {
                return DateTime.Parse(s);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
