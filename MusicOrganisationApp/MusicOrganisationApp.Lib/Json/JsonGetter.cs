using MusicOrganisationApp.Lib.Responses;
using System.Text.Json;

namespace MusicOrganisationApp.Lib.Json
{
    public static class JsonGetter
    {
        public static IEnumerable<T> GetValuesFromResponse<T, TResponse>(string json) where TResponse : IResponse<T>
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

        public static IEnumerable<T> GetFromFile<T, TResponse>(string filePath) where TResponse : IResponse<T>
        {
            string json = File.ReadAllText(filePath);
            return GetValuesFromResponse<T, TResponse>(json);
        }

        public async static Task<IEnumerable<T>> GetFromUrl<T, TResponse>(string url) where TResponse : IResponse<T>
        {
            string json = await WebReader.DownloadText(url);
            return GetValuesFromResponse<T, TResponse>(json);
        }

        public static T? GetFromFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}