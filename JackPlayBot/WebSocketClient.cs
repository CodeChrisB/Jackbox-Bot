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
            byte[] buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                    OnDataReceived?.Invoke(message);
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