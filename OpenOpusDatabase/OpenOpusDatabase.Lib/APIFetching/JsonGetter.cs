using OpenOpusDatabase.Lib.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.APIFetching
{
    public static class JsonGetter
    {
        public static List<T> GetValuesFromResponse<T, TResponse>(string json) where TResponse : IResponse<T>
        {
            TResponse? response = JsonSerializer.Deserialize<TResponse>(json);
            if (response != null)
            {
                return response.Values;
            }
            else
            {
                throw new Exception("null response");
            }
        }

        public static List<T> GetFromFile<T, TResponse>(string filePath) where TResponse : IResponse<T>
        {
            string json = File.ReadAllText(filePath);
            return GetValuesFromResponse<T, TResponse>(json);
        }

        public static List<T> GetFromUrl<T, TResponse>(string url) where TResponse : IResponse<T>
        {
            string json = WebReader.DownloadText(url);
            return GetValuesFromResponse<T, TResponse>(json);
        }

        public static T? GetFromFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}