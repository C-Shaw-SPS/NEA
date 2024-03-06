namespace MusicOrganisationApp.Lib.Json
{
    internal static class WebReader
    {
        public async static Task<(bool suceeded, string text)> TryDownloadTextAsync(string url)
        {
            using (HttpClient client = new())
            {
                HttpRequestMessage message = new(HttpMethod.Get, url);
                try
                {
                    HttpResponseMessage response = await client.SendAsync(message);
                    string text = GetDataFromResponse(response);
                    return (true, text);
                }
                catch (HttpRequestException)
                {
                    return (false, string.Empty);
                }
            }
        }

        private static string GetDataFromResponse(HttpResponseMessage response)
        {
            string text;
            using (StreamReader reader = new(response.Content.ReadAsStream()))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
    }
}