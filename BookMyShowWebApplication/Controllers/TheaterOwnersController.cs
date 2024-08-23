using BookMyShowWebApplication.Hub;
using BookMyShowWebApplication.Signalr;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Theater;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "Theaters")]
    public class TheaterOwnersController : ControllerBase
    {
        public IConfiguration _config;
        public ITheaterManage _theater;
        private IHubContext<MessageHub, IMessageHubClient> _hubcontext;
        public TheaterOwnersController(IConfiguration config,ITheaterManage theater, IHubContext<MessageHub, IMessageHubClient> hubcontext)
        {
            _config = config; 
            _theater= theater;
            _hubcontext= hubcontext;
        }
        [HttpPost]
        public async Task<List<TheatersDto>> AddNewTheater(TheatersDto theater)
        {
            var data = await _theater.AddNewTheater(theater);
            return data;
        }
        [HttpPost]
        public async Task<List<ScreenDto>> AddNewScreen(ScreenDto screen)
        {
            var data = await _theater.AddNewScren(screen).ConfigureAwait(false);
            return data;

        }
        [HttpGet]
        //[AllowAnonymous]
        public async Task<List<TheatersDto>> GetTheaterWithScreens()
        {
            var data =await _theater.GetTheaterWithScreens();
            return data;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<List<Showtime>> AddNewShow(Showtime screen)
        {
            List<Showtime> result = new List<Showtime>();
            try
            {
                var data = await _theater.AddShow(screen).ConfigureAwait(false);
                if (data != null)
                {
                    string message ="hello how are you";
                    var messages = new Notificationdto() { title = "hello" };
                   await _hubcontext.Clients.All.subscribernotification(messages);
           
                }
                return data;
            }
            catch (Exception ex) {
                return result;
            }


          
        }
    }
}
