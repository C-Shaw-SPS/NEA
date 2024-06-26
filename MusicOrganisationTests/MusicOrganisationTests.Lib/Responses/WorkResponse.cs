﻿using MusicOrganisationTests.Lib.Tables;
using MusicOrganisationTests.Lib.Converters;
using System.Text.Json.Serialization;

namespace MusicOrganisationTests.Lib.Responses
{
    internal class WorkResponse : IResponse<WorkData>
    {
        private IEnumerable<WorkData> _data = new List<WorkData>();

        [JsonPropertyName("works"), JsonConverter(typeof(DictionaryToIEnumerableConverter<string, WorkData>))]
        public IEnumerable<WorkData> Values
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}