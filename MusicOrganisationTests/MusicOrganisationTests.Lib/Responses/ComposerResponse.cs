﻿using MusicOrganisationTests.Lib.Models;
using System.Text.Json.Serialization;

namespace MusicOrganisationTests.Lib.Responses
{
    internal class ComposerResponse : IResponse<Composer>
    {
        private IEnumerable<Composer> _values = new List<Composer>();

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