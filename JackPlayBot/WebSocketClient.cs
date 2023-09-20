using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketClient
{
    private ClientWebSocket webSocket;

    public event Action<string> OnDataReceived;

    public WebSocketClient()
    {
        webSocket = new ClientWebSocket();
        int allowedBytes = 1024 * 1024 * 5;
        webSocket.Options.SetBuffer(allowedBytes, allowedBytes); ; // 1MB (adjust as needed)
    }

    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        try
        {
            webSocket.Options.AddSubProtocol("ecast-v0");
            await webSocket.ConnectAsync(uri, cancellationToken);

            if (webSocket.State == WebSocketState.Open)
            {
                Console.WriteLine("WebSocket is connected");
            }
            else
            {
                Console.WriteLine($"WebSocket is in state: {webSocket.State}");
            }

            StartListeningForMessages();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async void StartListeningForMessages()
    {
        try
        {
            const int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            MemoryStream messageBuffer = new MemoryStream();

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    return;
                }

                messageBuffer.Write(buffer, 0, result.Count);

                if (result.EndOfMessage)
                {
                    string message = System.Text.Encoding.UTF8.GetString(messageBuffer.ToArray());
                    OnDataReceived?.Invoke(message);
                    messageBuffer.SetLength(0); 

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            // Handle the exception
        }
    }

    public async Task SendMessageAsync(string message, CancellationToken cancellationToken)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
    }

    public async Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
    {
        await webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
    }
}