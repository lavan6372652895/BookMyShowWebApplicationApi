using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using BookMyShowWebApplicationServices.Interface.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        public IConfiguration _config;
        public IUserServices _serivices;
        public usersController(IConfiguration config, IUserServices serivices)
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

    }
}
