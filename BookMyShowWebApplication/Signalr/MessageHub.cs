using BookMyShowWebApplicationModal;
using Microsoft.AspNetCore.SignalR;
namespace BookMyShowWebApplication.Signalr
{
    public class MessageHub:Hub
    {
        public async Task UpdateSeatAvailability(List<SeatesDto> seats)
        {
            await Clients.All.SendAsync("UpdateSeats", seats);
        }
    }
}
