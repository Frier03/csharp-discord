# CSharp-Discord (Self Bot)

## Example
```c
// Create an instance of BotClient which DiscordClient is inherited into
BotClient _client = new BotClient();

// Call LoginAsync
await _client.LoginAsync("Token");
```

### Virtual methods
```c
// Personal Implementation of DiscordClient base class
public class BotClient : DiscordClient
{
    // This is essential since it will create more obvious code
    // and not offloading the event code into an unrelated class..
    public override void OnLoginSuccessful(DiscordClient client)
    {
        Console.WriteLine(client.user.username + " has successfully logged in!");
    }
}
```
