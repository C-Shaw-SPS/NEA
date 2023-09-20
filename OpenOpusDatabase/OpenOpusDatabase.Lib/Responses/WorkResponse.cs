using OpenOpusDatabase.Lib.Models;
using OpenOpusDatabase.Lib.Converters;
using System.Text.Json.Serialization;

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
