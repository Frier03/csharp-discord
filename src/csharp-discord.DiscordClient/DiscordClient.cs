using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Xml.Linq;

namespace csharp_discord.DiscordClient;

public class DiscordClient
{
    enum ErrorFlags : ushort
    {
        Warning = 8625
    }
    enum ClientStatus : ushort
    {
        Offline = 0,
        Online = 1
    }

    private HttpClient httpClient = new HttpClient();
    public User user = new User();

    public DiscordClient()
    {
        // SET HEADERS
        httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US");
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) discord/0.0.309 Chrome/83.0.4103.122 Electron/9.3.5 Safari/537.36");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }


    public async Task LoginAsync(String token)
    {
        httpClient.DefaultRequestHeaders.Add("Authorization", token);                           // SET AUTHORIZATION HEADER
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };    // SSL CERTIFICATE BYPASS

        // Request a response
        HttpResponseMessage HttpResponse = await APIManager.send_request(httpClient, "GET", $"users/@me");
        if(HttpResponse.StatusCode == HttpStatusCode.OK) {

            String payload = await HttpResponse.Content.ReadAsStringAsync();
            var payloadObject = System.Text.Json.JsonDocument.Parse(payload);
            //retrieve the value
            var username = payloadObject.RootElement.GetProperty("username");
            var discriminator = payloadObject.RootElement.GetProperty("discriminator");
            var id = payloadObject.RootElement.GetProperty("id");

            user.username = username.ToString();
            user.discriminator = discriminator.ToString();
            user.id = id.ToString();

            OnLoginSuccessful(this); 
        }

    }

    /////////////////////////// EVENTS ///////////////////////////
    public virtual void OnLoginSuccessful(DiscordClient client = null) { }
}