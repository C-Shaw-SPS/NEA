using MusicOrganisationApp.Lib.Tables;
using System.Text.Json.Serialization;

namespace MusicOrganisationApp.Lib.Responses
{
    internal class ComposerResponse : IResponse<ComposerData>
    {
        private IEnumerable<ComposerData> _values = new List<ComposerData>();

        [JsonPropertyName("composers")]
        public IEnumerable<ComposerData> Values
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