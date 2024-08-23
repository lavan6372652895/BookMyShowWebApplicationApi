using BookMyShowWebApplication.Hub;
using BookMyShowWebApplicationModal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace BookMyShowWebApplication.Signalr
{
   
    public class MessageHub: Hub<IMessageHubClient>
    {
        public async Task subscribernotification(Notificationdto message)
        {
            
            await Clients.All.subscribernotification(message);
        }
        public override Task OnConnectedAsync()
        {
           
            //Groups.AddToGroupAsync(); ;
            return base.OnConnectedAsync();
        }

    }
}
