using OpenOpusDatabase.Lib.Models;
using System.Text.Json.Serialization;

namespace OpenOpusDatabase.Lib.Responses
{
    internal class ComposerResponse : IResponse<Composer>
    {
        private IEnumerable<Composer> _values;

        [JsonPropertyName("composers")]
        public IEnumerable<Composer> Values
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