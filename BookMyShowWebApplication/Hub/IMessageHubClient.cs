using BookMyShowWebApplicationModal;
using Microsoft.AspNetCore.Authorization;
namespace BookMyShowWebApplication.Hub
{
   
    public interface IMessageHubClient
    {
        Task subscribernotification(Notificationdto message);
        Task SendernotificationToUser(Notificationdto message);
        Task sendNotificationListofusers(Notificationdto message);
    }
}
