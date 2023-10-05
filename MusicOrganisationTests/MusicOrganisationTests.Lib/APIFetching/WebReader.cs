namespace MusicOrganisationTests.Lib.APIFetching
{
    internal static class WebReader
    {
        public static string DownloadText(string url)
        {
            string text;
            using (HttpClient client = new())
            {
                HttpRequestMessage message = new(HttpMethod.Get, url);
                HttpResponseMessage response = client.Send(message);
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