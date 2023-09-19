using PlayerBots.Common.Data;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Net.Http.Json;

namespace PlayerBots
{
    public class BotPlayer
    {
        private TcpClient client;
        private NetworkStream stream;
        private Intelligence intelligence;
        private Guid PlayerId;

        public BotPlayer(Intelligence intelligence)
        {
            this.intelligence = intelligence;
            PlayerId = Guid.NewGuid();
        }

        public async void PlayGame(string roomCode, string userName)
        {

                string roomId = "";
                //get hostId
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://ecast.jackboxgames.com/api/v2/rooms/{roomCode}");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

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
                    await client.SendMessageAsync("Hello, server!", cts.Token);
                }



        }

        private void OnDataReceived(string obj)
        {
            //dynamic jsonData = JsonConvert.DeserializeObject(obj);
            Console.WriteLine(obj);
        }
    }
}
