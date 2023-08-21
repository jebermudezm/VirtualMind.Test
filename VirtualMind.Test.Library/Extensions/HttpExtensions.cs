using System.Text.Json;

namespace VirtualMind.Test.Library.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<T> SendResponseAsync<T>(this HttpClient client, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var response = await client.SendAsync(request);
            return await response.ReadContentAs<T>();
        }

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(content);
        }




    }
}
