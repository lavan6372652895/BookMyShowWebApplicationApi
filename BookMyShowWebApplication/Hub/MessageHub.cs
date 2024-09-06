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
        public async Task SendernotificationToUser(Notificationdto message ,string userId)
        {
         
            await Clients.User(userId).SendernotificationToUser(message);
        }
        public async Task sendNotificationListofusers(Notificationdto message,List<string>userid) {

            await Clients.Users(userid).sendNotificationListofusers(message);
        
        }


    }
}
