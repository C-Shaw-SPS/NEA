using MusicOrganisationTests.Lib.Models;
using MusicOrganisationTests.Lib.Responses;

namespace MusicOrganisationTests.Lib.APIFetching
{
    public static class ComposerGetter
    {
        private const string _URL = "https://api.openopus.org/composer/list/search/.json";
        public static IEnumerable<Composer> GetFromOpenOpus()
        {
            return JsonGetter.GetFromUrl<Composer, ComposerResponse>(_URL);
        }

        public static IEnumerable<Composer> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<Composer, ComposerResponse>(filePath);
        }
    }
}
