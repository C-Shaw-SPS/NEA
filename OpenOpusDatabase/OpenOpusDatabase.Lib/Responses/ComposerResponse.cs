using OpenOpusDatabase.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
