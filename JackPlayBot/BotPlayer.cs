using PlayerBots.Common.Data;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using JackPlayBot.Common.Register.ActionCtx.Model;
using JackPlayBot.Common.Register.ActionCtx.Helper;
using JackPlayBot.Common.Register;
using JackPlayBot.Common.Data;

namespace PlayerBots
{
    public class BotPlayer
    {
        private TcpClient client;
        private NetworkStream stream;
        private Intelligence intelligence;
        private Guid PlayerId;
        private Games currentGame;

        public BotPlayer(Intelligence intelligence)
        {
            this.intelligence = intelligence;
            PlayerId = Guid.NewGuid();
        }

        public async Task<bool> PlayGame(string roomCode, string userName, Games game)
        {
            currentGame = game;
            roomCode = roomCode.ToUpper();
            string roomId = String.Empty;
            //get hostId
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://ecast.jackboxgames.com/api/v2/rooms/{roomCode}");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd().ToString();

                    // Parse the JSON content to get the specific value you need
                    dynamic jsonData = JsonConvert.DeserializeObject(jsonContent);
                    roomId = jsonData.body.host;

                    WebSocketClient client = new WebSocketClient();
                    client.OnDataReceived += OnDataReceived;
                    Uri uri = new Uri($"wss://{roomId}/api/v2/rooms/{roomCode}/play?role=player&name={userName}&format=json&user-id={PlayerId}");
                    CancellationTokenSource cts = new CancellationTokenSource();

                    await client.ConnectAsync(uri, cts.Token);
                    Console.WriteLine("Connected to WebSocket.");
                }
                return true;
            }
            catch (WebException exception)
            {
                Console.WriteLine("Wrong room number or no connection");
                return false;
            }
        }

        private void OnDataReceived(string obj)
        {
            ActionContext context = ActionContextBuilder.Build(obj, intelligence);


            ContextDistributer.CallFunction(context, currentGame);
        }
    }
}
