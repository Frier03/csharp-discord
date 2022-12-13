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

    /// <summary>
    /// Gets the newest message sent in the channel
    /// Recursive Function
    /// </summary>
    /// <param name="client"></param>
    /// <param name="channel_id"></param>
    /// <param name="last_message_id"></param>
    /// <returns> Returns a JToken with the message data </returns>
    async static public Task<String> getLatestMessage(HttpClient client, string channel_id, string last_message_id)
    {
        HttpResponseMessage response = await send_request(client, "GET", $"channels/{channel_id}/messages?limit=1");
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Sends a request to the discord gateway and receive opcodes
    /// </summary>
    /// <param name="client"></param>
    /// <param name="method"></param>
    /// <param name="request_url"></param>
    /// <param name="body"></param>
    /// <returns> Returns a HttpResponseMessage </returns>
    async static public Task<HttpResponseMessage> receive_events(HttpClient client, HttpContent body = null)
    {
        HttpResponseMessage response = null;
        response = await client.PostAsync("wss://gateway.discord.gg/", body);

        return response;
    }
}