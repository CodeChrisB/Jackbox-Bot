using PlayerBots;
using PlayerBots.Common.Data;

namespace BotStarter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Room Code: ");
            string roomCode = Console.ReadLine();

            for (int i = 0; i < 8; i++)
                new BotPlayer(Intelligence.BadPlayer)
                    .PlayGame(roomCode, "Bot");



            await Task.Delay(-1);
        }
    }
}