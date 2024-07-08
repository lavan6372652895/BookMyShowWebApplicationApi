using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IConfiguration _config;
        public IAdminServices _Services;
        public AdminController(IConfiguration config, IAdminServices service)
        {
            _config = config;
            _Services = service;
        }
        [HttpPost]
        public async Task<List<MoviesDto>> AddNewmovies(MoviesDto movies)
        {

            movies.Moviecast.ToString();
            var data=_Services.AddNewMovie(movies);
            if (data != null)
            {
                return data.Result.ToList();
            }
            else { 
            return data.Result.ToList();
            }

        }
        [HttpGet]
        public async Task<List<GenreDto>> GetAllGenre()
        {
            var data = _Services.GetListofGenre();
            return data.Result.ToList();
        }
        [HttpPost]
        public async Task<List<ActorDto>> AddNewActor(ActorDto act)
        {
            var data =  _Services.AddNewActor(act);
            if (data != null) {

                return data.Result.ToList();

            }
            else {
                return data.Result.ToList();
            }
            
        }

    }
}
