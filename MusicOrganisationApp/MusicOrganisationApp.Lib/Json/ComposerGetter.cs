using MusicOrganisationApp.Lib.Responses;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.Json
{
    public static class ComposerGetter
    {
        private const string _URL = "https://api.openopus.org/composer/list/search/.json";
        public static async Task<IEnumerable<Composer>> GetFromOpenOpus()
        {
            return await JsonGetter.GetFromUrl<Composer, ComposerResponse>(_URL);
        }

        public static IEnumerable<Composer> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<Composer, ComposerResponse>(filePath);
        }
    }
}
