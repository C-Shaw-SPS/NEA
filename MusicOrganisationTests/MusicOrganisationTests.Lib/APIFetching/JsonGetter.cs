using MusicOrganisationTests.Lib.Responses;
using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("MusicOrganisationTests.Tests")]
namespace MusicOrganisationTests.Lib.APIFetching
{
    internal static class JsonGetter
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

        public static IEnumerable<T> GetFromUrl<T, TResponse>(string url) where TResponse : IResponse<T>
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