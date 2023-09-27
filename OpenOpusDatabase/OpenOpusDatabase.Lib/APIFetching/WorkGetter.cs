using OpenOpusDatabase.Lib.Models;
using OpenOpusDatabase.Lib.Responses;

namespace OpenOpusDatabase.Lib.APIFetching
{
    public static class WorkGetter
    {
        private const string _URL = "https://api.openopus.org/work/list/ids/.json";

        public static IEnumerable<Work> GetFromOpenOpus()
        {
            return JsonGetter.GetFromUrl<Work, WorkResponse>(_URL);
        }

        public static IEnumerable<Work> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<Work, WorkResponse>(filePath);
        }
    }
}