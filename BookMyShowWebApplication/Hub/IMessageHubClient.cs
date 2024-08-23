using BookMyShowWebApplicationModal;
using Microsoft.AspNetCore.Authorization;
namespace BookMyShowWebApplication.Hub
{
   
    public interface IMessageHubClient
    {
        Task subscribernotification(Notificationdto message);
    }
}
