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
        output.WriteLine("Creating DiscordClient Instance");
        // Create an instance of BotClient which _DiscordClient is inherited into
        DiscordClient client = new DiscordClient();

        output.WriteLine("Trying to login");
        
        // Call Login
        client.LoginAsync("TOKEN");

        client.ListenFor(DiscordClient.DiscordEvent.READY, data =>
        {
            Ready readyData = (Ready)data;
            output.WriteLine(readyData.d.user.username + " has logged in!");
        });

        // Block this task until the program is closed manually (Comment it out, if running unit test)
        //await Task.Delay(-1);
    }
}