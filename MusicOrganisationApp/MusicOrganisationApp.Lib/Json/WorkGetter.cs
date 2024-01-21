﻿using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.Responses;

namespace MusicOrganisationApp.Lib.Json
{
    public static class WorkGetter
    {
        private const string _URL = "https://api.openopus.org/work/list/ids/.json";

        public static async Task<IEnumerable<WorkData>> GetFromOpenOpus()
        {
            return await JsonGetter.GetFromUrl<WorkData, WorkResponse>(_URL);
        }

        public static IEnumerable<WorkData> GetFromFile(string filePath)
        {
            return JsonGetter.GetFromFile<WorkData, WorkResponse>(filePath);
        }
    }
}