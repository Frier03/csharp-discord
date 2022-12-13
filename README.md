# CSharp-Discord

```c
// Create an instance of BotClient which _DiscordClient is inherited into
BotClient _client = new BotClient();

// Call LoginAsync
await _client.LoginAsync("Token");

// Block this task until the program is closed manually (Comment it out, if running unit test)
await Task.Delay(-1);
```

```c
// Personal Implementation of _DiscordClient base class
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
