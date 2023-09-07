using OpenOpusDatabase.Lib.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenOpusDatabase.Lib.APIFetching
{
    internal static class JsonGetter
    {
        public static List<T> GetValuesFromResponse<T, TResponse>(string json, JsonSerializerOptions options = null) where TResponse : IResponse<T>
        {
            TResponse? response = JsonSerializer.Deserialize<TResponse>(json, options);
            if (response != null)
            {
                return response.Values;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static List<T> GetFromFile<T, TResponse>(string filePath, JsonSerializerOptions options = null) where TResponse : IResponse<T>
        {
            string json = File.ReadAllText(filePath);
            return GetValuesFromResponse<T, TResponse>(json, options);
        }

        public static List<T> GetFromUrl<T, TResponse>(string url, JsonSerializerOptions options = null) where TResponse : IResponse<T>
        {
            string json = WebReader.DownloadText(url);
            return GetValuesFromResponse<T, TResponse>(json, options);
        }
    }
}
