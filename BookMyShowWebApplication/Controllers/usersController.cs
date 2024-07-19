using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.Users;
using BookMyShowWebApplicationServices.Interface.IHome;
using BookMyShowWebApplicationServices.Interface.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IConfiguration _config;
        public IUserServices _serivices;
        public UsersController(IConfiguration config, IUserServices serivices)
        {
            _config = config;
            _serivices = serivices;
        }

        [HttpGet]
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
        public Task<string> AddBooking(Bookingsdto[] booking)
        {
            var data = _serivices.Addseat(booking);
            return Task.FromResult(data.Result);
        }
    }
}
