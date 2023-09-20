using JackPlayBot;
using PlayerBots;
using PlayerBots.Common.Data;

namespace BotStarter
{
    public class Program
    {
        static async Task Main(string[] args)
        {


            PlayBotSetup.SetUp();
            Console.Write("Room Code: ");
            string roomCode = Console.ReadLine();

                new BotPlayer(Intelligence.BadPlayer)
                    .PlayGame(roomCode, "Bot",JackPlayBot.Common.Data.Games.Guesspionage);



            await Task.Delay(-1);
        }
    }
}