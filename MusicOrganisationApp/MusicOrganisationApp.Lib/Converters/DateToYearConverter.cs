﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Converters
{
    internal class DateToYearConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(int?))
            {
                return GetDateTime(ref reader);
            }
            else
            {
                throw new JsonException();
            }
        }

        private static int? GetDateTime(ref Utf8JsonReader reader)
        {
            string? s = reader.GetString();
            if (s is not null && DateTime.TryParse(s, out DateTime dateTime))
            {
                return dateTime.Year;
            }
            else
            {
                throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}