# CSharp-Discord (Self Bot)

## Example
```c
// Create an instance of BotClient which DiscordClient is inherited into
DiscordClient client = new DiscordClient();

// Call LoginAsync
await client.LoginAsync("Token");
```

### Listen for events
```c
client.ListenFor(DiscordClient.DiscordEvent.READY, data =>
{
    Ready readyData = (Ready)data;
    output.WriteLine(readyData.d.user.username + " has logged in!");
});

client.ListenFor(DiscordClient.DiscordEvent.MESSAGE_CREATE, data =>
{
    Message messageData = (Message)data;
    output.WriteLine(messageData.d.content);
});
```
