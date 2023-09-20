using OpenOpusDatabase.Lib.Models;
using System.Text.Json.Serialization;

namespace OpenOpusDatabase.Lib.Responses
{
    public class ComposerResponse : IResponse<Composer>
    {
        private List<Composer> _values;

        [JsonPropertyName("composers")]
        public List<Composer> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }
    }
}
