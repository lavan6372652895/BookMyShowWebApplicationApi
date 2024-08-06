using BookMyShowWebApplication.Hub;
using BookMyShowWebApplicationModal;
using Microsoft.AspNetCore.SignalR;
namespace BookMyShowWebApplication.Signalr
{
    public class MessageHub: Hub<IMessageHubClient>
    {
        public async Task SendOffersToUser(List<string> message)
        {
            await Clients.All.SendOffersToUser(message);
        }
    }
}
