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
        public IConfiguration _config;
        public IUserServices _serivices;
        private readonly IHubContext<MessageHub> _hubContext;
        public UsersController(IConfiguration config, IUserServices serivices, IHubContext<MessageHub> hubContext)
        {
            _config = config;
            _serivices = serivices;
            _hubContext = hubContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<List<MoviesDto>> GetListofMovies() {

            var data = _serivices.MoviesList();
            if (data.Result.Count > 0)
            {
                return Task.FromResult(data.Result);
            }
            else
            {
                return data;
            }
        }
        [HttpGet]
        public Task<List<ListofMovieTheaterscs>> TheaterList(int movieid, int cityid)
        {
            //string token = HttpContext.Request.Headers.Authorization;
            //string? nameIdentifier = _customUserIdProvider.GetUserId(HttpContext.Connection);

            var data = _serivices.moviesListOfTheaterList(movieid, cityid);
            if (data.Result.Count > 0)
            {
                return Task.FromResult(data.Result);
            }
            else
            {
                return data;
            }
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
            var data = _serivices.Addseat(booking);
          // await _hubContext.Clients.All.UpdateSeatAvailability();
            return data.Result;
        }
    }
}
