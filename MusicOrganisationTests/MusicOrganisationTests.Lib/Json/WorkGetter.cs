using MusicOrganisationTests.Lib.Tables;
using MusicOrganisationTests.Lib.Responses;

namespace MusicOrganisationTests.Lib.Json
{
    public static class WorkGetter
    {
        private const string _URL = "https://api.openopus.org/work/list/ids/.json";

        public static IEnumerable<WorkData> GetFromOpenOpus()
        {
            return JsonGetter.GetFromUrl<WorkData, WorkResponse>(_URL);
        }

        public static IEnumerable<WorkData> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<WorkData, WorkResponse>(filePath);
        }
    }
}