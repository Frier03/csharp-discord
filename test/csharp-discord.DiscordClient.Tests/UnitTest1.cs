using Xunit;
using csharp_discord;
using Xunit.Abstractions;

namespace csharp_discord.DiscordClient.Tests;


// Main Class
public class Foo
{
    private readonly ITestOutputHelper output;

    public Foo(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public async void Main()
    {
        output.WriteLine("Creating BotClient Instance");
        // Create an instance of BotClient which _DiscordClient is inherited into
        BotClient client = new BotClient(output);

        output.WriteLine("Trying to login");
        // Call LoginAsync
        //NjIyODM2ODA5ODg5NzQyODQ5.GJisoX.3RwBTq7U8_l-vxUbowbyXcKfVbtYtTT4n5d7yI
        await client.LoginAsync("Token");

        // Read Channel Messages
        await client.readChannelMessages();

        // Block this task until the program is closed manually (Comment it out, if running unit test)
        //await Task.Delay(-1);
    }
}

// Personal Implementation of _DiscordClient base class
public class BotClient : DiscordClient
{
    private readonly ITestOutputHelper output;

    public BotClient(ITestOutputHelper output)
    {
        this.output = output;
    }

    // This is essential since it will create more obvious code
    // and not offloading the event code into an unrelated class..
    public override void OnLoginSuccessful(DiscordClient client)
    {
        output.WriteLine(client.user.username + " has successfully logged in!");
    }

    public override void OnChannelMessages(DiscordClient client, Message message)
    {
        output.WriteLine(client.user.username + " : " + message.author.username);
    }
}