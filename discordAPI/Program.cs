using Discord;
using Discord.WebSocket;
using System.Text;

DiscordSocketClient client;

client = new DiscordSocketClient();
client.MessageReceived += CommandHandler;
client.Log += Log;


var token = "token";

await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

Console.ReadLine();


Task Log(LogMessage message)
{
    Console.WriteLine(message.ToString());
    return Task.CompletedTask;
}

Task CommandHandler(SocketMessage arg)
{
    if (!arg.Author.IsBot)
    {
        string msg = arg.Content.ToLower();
        object command = null;
        Random random = new Random();

        var channel = arg.Channel as SocketGuildChannel;
        IGuild guild = client.GetGuild(channel.Guild.Id);
        
        List<string> split_message = new List<string>(msg.Split(' '));
        List<IGuildUser> users = new List<IGuildUser>(guild.GetUsersAsync().Result.ToList().FindAll(i => !i.IsBot));

        if (split_message.Contains("кого") && split_message.Contains("баним?"))
            command = 1;
        else
            command = arg.Content;
       
        switch (command)
        {
            case 1:
            case "!ban":
                
                List<ulong> users_id = new List<ulong>(users.FindAll(i => !i.IsBot).Select(i => i.Id));

                #region
                /*foreach (var item in users)
                {
                    if (!item.IsBot)
                    {
                        users_id.Add(item.Id);
                    }
                }*/
                #endregion

                int ban_id = random.Next(users_id.Count);

                foreach (var item in users)
                {
                    if (item.Id == users_id[ban_id] && !item.IsBot)
                    {
                        arg.Channel.SendMessageAsync($"Бан получает: {item.Username}");
                    }
                }
                break;
        }
    }
    return Task.CompletedTask;
}
