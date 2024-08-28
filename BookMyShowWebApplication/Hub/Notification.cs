//using Microsoft.AspNet.SignalR.WebSockets;
//using System.Net.WebSockets;
//using System.Text;

//namespace BookMyShowWebApplication.Hub
//{
//    public class ChatHub :WebSocketHandler
//    {
//        public ChatHub(int? maxIncomingMessageSize) : base(maxIncomingMessageSize)
//        {

//        }

//        public  async Task OnConnectedAsync(WebSocket webSocket)
//        {
//            // Handle new connection
//            await SendMessageAsync(webSocket, "Welcome to our page");
//        }

//        public  async Task OnDisconnectedAsync(WebSocket webSocket, Exception exception)
//        {
//            // Handle disconnected client
//        }

//        public  async Task OnReceivedAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
//        {
//            // Handle incoming message
//            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
//            await BroadcastMessageAsync(message);
//        }

//        private async Task SendMessageAsync(WebSocket socket, string message)
//        {
//            var buffer = Encoding.UTF8.GetBytes(message);
//            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
//        }

//        private async Task BroadcastMessageAsync(string message)
//        {
//            // Send message to all connected clients
//        }
//    }

//}
