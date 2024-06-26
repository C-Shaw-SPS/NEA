﻿using MusicOrganisationTests.Lib.Responses;
using MusicOrganisationTests.Lib.Tables;

namespace MusicOrganisationTests.Lib.Json
{
    public static class ComposerGetter
    {
        private const string _URL = "https://api.openopus.org/composer/list/search/.json";
        public static IEnumerable<ComposerData> GetFromOpenOpus()
        {
            return JsonGetter.GetFromUrl<ComposerData, ComposerResponse>(_URL);
        }

        public static IEnumerable<ComposerData> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<ComposerData, ComposerResponse>(filePath);
        }
    }
}
