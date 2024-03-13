using MusicOrganisationApp.Lib.Responses;
using System.Text.Json;

namespace MusicOrganisationApp.Lib.Json
{
    public static class JsonGetter
    {
        public static IEnumerable<T> GetValuesFromResponse<T, TResponse>(string json) where TResponse : IResponse<T>
        {
            TResponse? response = JsonSerializer.Deserialize<TResponse>(json);
            if (response is not null)
            {
                return response.Values;
            }
            else
            {
                return [];
            }
        }

        public static IEnumerable<T> GetFromFile<T, TResponse>(string filePath) where TResponse : IResponse<T>
        {
            string json = File.ReadAllText(filePath);
            return GetValuesFromResponse<T, TResponse>(json);
        }

        public async static Task<IEnumerable<T>> GetFromUrlAsync<T, TResponse>(string url) where TResponse : IResponse<T>
        {
            (bool succeeded, string json) = await WebReader.TryDownloadTextAsync(url);
            if (succeeded)
            {
                IEnumerable<T> values = GetValuesFromResponse<T, TResponse>(json);
                return values;
            }
            else
            {
                return [];
            }
        }

        public static T? GetFromFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}