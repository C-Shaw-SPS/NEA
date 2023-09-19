using OpenOpusDatabase.Lib.Models;
using OpenOpusDatabase.Lib.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.APIFetching
{
    public static class WorkGetter
    {
        private const string _URL = "https://api.openopus.org/work/list/ids/.json";

        public static List<Work> GetFromOpenOpus()
        {
            return JsonGetter.GetFromUrl<Work, WorkResponse>(_URL);
        }

        public static List<Work> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<Work, WorkResponse>(filePath);
        }
    }
}
