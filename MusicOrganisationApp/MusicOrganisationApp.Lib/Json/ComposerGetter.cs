using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.Responses;

namespace MusicOrganisationApp.Lib.Json
{
    public static class ComposerGetter
    {
        private const string _URL = "https://api.openopus.org/composer/list/search/.json";
        public static async Task<IEnumerable<ComposerData>> GetFromOpenOpus()
        {
            return await JsonGetter.GetFromUrl<ComposerData, ComposerResponse>(_URL);
        }

        public static IEnumerable<ComposerData> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<ComposerData, ComposerResponse>(filePath);
        }
    }
}
