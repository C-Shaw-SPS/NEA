using OpenOpusDatabase.Lib.Converters;
using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.Responses
{
    public class WorkResponse : IResponse<Work>
    {
        private List<Work> _data = new();

        [JsonPropertyName("works"), JsonConverter(typeof(DictionaryToListConverter<string, Work>))]
        public List<Work> Values
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
