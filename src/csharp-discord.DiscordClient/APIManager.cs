using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace csharp_discord.DiscordClient;

internal class APIManager
{
    private static readonly string API_URL = "https://discord.com/api/";

    /// <summary>
    /// Sends a request to the url using a http client
    /// </summary>
    /// <param name="client"></param>
    /// <param name="method"></param>
    /// <param name="request_url"></param>
    /// <param name="body"></param>
    /// <returns> Returns a HttpResponseMessage </returns>
    async static public Task<HttpResponseMessage> send_request(HttpClient client, string method, string request_url, HttpContent body = null)
    {
        HttpResponseMessage response = null;
        switch (method.ToUpper())
        {
            case "GET":
                {
                    response = await client.GetAsync(API_URL + request_url);
                    break;
                }
            case "POST":
                {
                    response = await client.PostAsync(API_URL + request_url, body);
                    break;
                }
        }

        return response;
    }
}