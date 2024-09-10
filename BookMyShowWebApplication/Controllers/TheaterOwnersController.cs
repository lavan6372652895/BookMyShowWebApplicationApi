using BookMyShowWebApplication.Hub;
using BookMyShowWebApplication.Signalr;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.Users;
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
    public class TheaterOwnersController : ControllerBase
    {
       
        private readonly ITheaterManage _theater;
        private readonly IHubContext<MessageHub, IMessageHubClient> _hubcontext;
        public TheaterOwnersController(ITheaterManage theater, IHubContext<MessageHub, IMessageHubClient> hubcontext)
        {
            _theater= theater;
            _hubcontext= hubcontext;
        }
        [HttpPost]
        [Authorize(Roles = "Theaters")]
        public async Task<List<TheatersDto>> AddNewTheater(TheatersDto theater)
        {
            var data = await _theater.AddNewTheater(theater);
            return data;
        }
        [HttpPost]
        [Authorize(Roles = "Theaters")]
        public async Task<List<ScreenDto>> AddNewScreen(ScreenDto screen)
        {
            var data = await _theater.AddNewScren(screen).ConfigureAwait(false);
            return data;

        }
        [HttpGet]
        [Authorize(Roles = "Theaters")]
        public async Task<List<TheatersDto>> GetTheaterWithScreens()
        {
            var data =await _theater.GetTheaterWithScreens();
            return data;
        }
        [HttpPost]
        [Authorize(Roles = "Theaters")]
        public async Task<List<Showtime>> AddNewShow(Showtime screen)
        {
            List<Showtime> result = new List<Showtime>();
            try
            {
                var data = await _theater.AddShow(screen).ConfigureAwait(false);
                if (data != null)
                {
                   
                    var messages = new Notificationdto() { title = "hello" };
                   await _hubcontext.Clients.All.subscribernotification(messages);
                   
                }
               return data?.ToList() ?? result;
            }
            catch {
                return result;
            }
        }

        [HttpGet]
        [Authorize]
        public Task<List<ListofMovieTheaterscs>> TheaterList(int movieid, int cityid)
        {
            //string token = HttpContext.Request.Headers.Authorization;
            //string? nameIdentifier = _customUserIdProvider.GetUserId(HttpContext.Connection);

            var data = _theater.moviesListOfTheaterList(movieid, cityid);
            if (data.Result.Count > 0)
            {
                return Task.FromResult(data.Result);
            }
            else
            {
                return data;
            }
        }
    }
}
