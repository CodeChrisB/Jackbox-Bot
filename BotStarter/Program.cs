using JackPlayBot;
using PlayerBots;
using PlayerBots.Common.Data;

namespace BotStarter
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            BotPlayer botPlayer = new BotPlayer(Intelligence.BadPlayer);
            PlayBotSetup.SetUp();
            bool result;

            do
            {
                Console.Write("Room Code: ");
                string roomCode = Console.ReadLine();
                result = await botPlayer.PlayGame(roomCode, "Bot",JackPlayBot.Common.Data.Games.Guesspionage);

            } while (!result);

            await Task.Delay(-1);
        }
    }
}