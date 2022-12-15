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

    // Define an enumeration of possible events that can be listened for
    public enum DiscordEvent
    {
        READY
    }

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
                    JsonElement json = JsonDocument.Parse(data).RootElement;

                    // Try to get the value of the "t" attribute
                    string? EventType = json.TryGetProperty("t", out JsonElement tValue) ? tValue.GetString() : null;

                    // Handle this event now
                    HandleEvent(EventType, data);
                }
            }
        }
    }

    // Define a dictionary that maps event type strings to the corresponding data types
    private readonly Dictionary<string, Type> _eventDataTypes = new Dictionary<string, Type>
        {
            { "READY", typeof(Ready) }
            // Add more event types here as needed
        };

    // Define a dictionary that maps event names to a list of actions to be performed when the event is triggered
    private readonly Dictionary<DiscordEvent, List<Action<object>>> _eventActions = new Dictionary<DiscordEvent, List<Action<object>>>();

    // Add an action to be performed when the specified event is triggered
    public void ListenFor(DiscordEvent eventName, Action<object> action)
    {
        // Add the action to the list of actions for this event
        if (!_eventActions.ContainsKey(eventName))
            _eventActions[eventName] = new List<Action<object>>();
        _eventActions[eventName].Add(action);
    }

    // Call all of the actions registered for the specified event
    private void OnEvent(DiscordEvent eventName, object data)
    {
        // Check if there are any actions registered for this event
        if (_eventActions.ContainsKey(eventName))
        {
            // Call all of the actions registered for this event
            foreach (var action in _eventActions[eventName])
            {
                action(data);
            }
        }
    }

    // Call the appropriate event handling method based on the event type
    private void HandleEvent(string eventType, string data)
    {
        if (eventType == null || data == null)
            return;

        // Try to get the corresponding DiscordEvent enum value for this event type string
        if (!Enum.TryParse(eventType, out DiscordEvent eventName))
            return;

        // Deserialize the data using the appropriate data type
        object deserializedData = JsonSerializer.Deserialize(data, _eventDataTypes[eventType]);

        // Call all of the actions registered for this event
        OnEvent(eventName, deserializedData);
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
}