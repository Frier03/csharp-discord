using System;
using System.Net;
using System.Net.WebSockets;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace csharp_discord.DiscordClient;

public class DiscordClient
{

    private ClientWebSocket webSocket = new ClientWebSocket();
    private string gatewayUrl = "wss://gateway.discord.gg";
    private Thread webSocketThread;

    public DiscordClient() { webSocketThread = new Thread(webSocketListener);  }

    private async void webSocketListener()
    {
        // Listen for events from the gateway (webSocket)
        while (webSocket.State == WebSocketState.Open)
        {
            /*
             * 
             * the response data from the Discord gateway is written to the MemoryStream, 
             * and then the stream is read using a StreamReader. The ReadToEnd method is 
             * used to read all of the data from the stream, and the data is deserialized 
             * into a Message object using the JsonSerializer.Deserialize method. Once the 
             * Message object has been created, it can be processed by the application.
             * ( Only took 4 hours to figure out (: ) 
             * 
             */

            // Receive the data from the gateway
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            // Write the data to a MemoryStream
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(buffer, 0, result.Count);

                // Check if there is any remaining data in the response
                while (result.EndOfMessage == false)
                {
                    // Receive the remaining data from the gateway
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    memoryStream.Write(buffer, 0, result.Count);
                }

                // Reset the stream position to the beginning
                memoryStream.Position = 0;

                // Read the data from the stream
                using (var streamReader = new StreamReader(memoryStream))
                {
                    var data = streamReader.ReadToEnd();

                    // Parse the JSON string
                    //JsonElement json = JsonDocument.Parse(data).RootElement;

                    // Try to get the value of the "t" attribute
                    //string? EventType = json.TryGetProperty("t", out JsonElement tValue) ? tValue.GetString() : null;

                    // Handle this event now
                    //CallEvent(EventType);

                    // Deserialize the JSON string into a Message object
                    //var message = System.Text.Json.JsonSerializer.Deserialize<Message>(data);
                }
            }
        }
    }

    private void CallEvent(string EventType)
    {
        if (EventType == "READY")
        {
            OnLoginSuccessful();
        }
    }

    public async void LoginAsync(String token)
    {
        /// <summary>
        /// Logs in to a Discord account and connects to the Discord gateway.
        /// </summary>
        /// <param name="token">The user token for the Discord account to log in to.</param>
        /// <triggers>Triggers the "OnLoginSuccessful" virtual method</triggers>

        // Create presence (will not affect anything, but needs to be there for the payload)
        dynamic presence = new { game = new { name = "" }, status = "online" };

        // Connect to the gateway URL
        await webSocket.ConnectAsync(new Uri(gatewayUrl), CancellationToken.None);

        // Serialize the presence object to a JSON string
        var presenceJson = JsonSerializer.Serialize(presence);

        // Send the authentication payload to the gateway
        // The payload should contain the user token and some other information
        // For more information about the authentication payload, see the Discord API documentation
        // In this example, we will just send a hard-coded payload for simplicity
        // Create the JSON string using the JsonConvert.ToString method
        var payload = string.Format("{{\"op\": 2, \"d\": {{\"token\": \"{0}\", \"properties\": {{}}, \"compress\": false, \"large_threshold\": 250, \"presence\": {1}}}}}", token, presenceJson);
        var payloadBytes = Encoding.UTF8.GetBytes(payload);
        await webSocket.SendAsync(new ArraySegment<byte>(payloadBytes), WebSocketMessageType.Text, true, CancellationToken.None);

        webSocketThread.Start();
    }


    /////////////////////////// VIRTUAL METHODS ///////////////////////////
    public virtual void OnLoginSuccessful() { }
}