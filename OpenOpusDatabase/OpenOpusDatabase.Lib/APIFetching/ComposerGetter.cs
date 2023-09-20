using OpenOpusDatabase.Lib.Models;
using OpenOpusDatabase.Lib.Responses;

namespace OpenOpusDatabase.Lib.APIFetching
{
    public static class ComposerGetter
    {
        private const string _URL = "https://api.openopus.org/composer/list/search/.json";
        public static List<Composer> GetFromOpenOpus()
        {
            return JsonGetter.GetFromUrl<Composer, ComposerResponse>(_URL);
        }

        public static List<Composer> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<Composer, ComposerResponse>(filePath);
        }
    }
}
