using MusicOrganisationTests.Lib.Tables;
using MusicOrganisationTests.Lib.Converters;
using System.Text.Json.Serialization;

namespace MusicOrganisationTests.Lib.Responses
{
    internal class WorkResponse : IResponse<Work>
    {
        private IEnumerable<Work> _data = new List<Work>();

        [JsonPropertyName("works"), JsonConverter(typeof(DictionaryToIEnumerableConverter<string, Work>))]
        public IEnumerable<Work> Values
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