using BookMyShowWebApplication.Signalr;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.Users;
using BookMyShowWebApplicationServices.Interface.IHome;
using BookMyShowWebApplicationServices.Interface.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserServices _serivices;
        private  IHubContext<MessageHub> _hubContext;
        public UsersController( IUserServices serivices, IHubContext<MessageHub> hubContext)
        {
            _serivices = serivices;
            _hubContext = hubContext;
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin,user")]
        public Task<List<SeatesDto>> GetListofSeates(int Showid)
        { 
            var data =_serivices.seatesList(Showid);
            if(data.Result.Count > 0)
            {
                return Task.FromResult(data.Result);
            }
            else { return data; }
        }

        [HttpPost]
        public  async Task<string> AddBooking(Bookingsdto[] booking)
        {
            var data =await _serivices.Addseat(booking);
          // await _hubContext.Clients.All.UpdateSeatAvailability();
            return data;
        }
    }
}
