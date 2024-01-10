namespace MusicOrganisation.Lib.Json
{
    internal static class WebReader
    {
        public async static Task<string> DownloadText(string url)
        {
            string text;
            using (HttpClient client = new())
            {
                HttpRequestMessage message = new(HttpMethod.Get, url);
                HttpResponseMessage response = await client.SendAsync(message);
                text = GetDataFromResponse(response);
            }
            return text;
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