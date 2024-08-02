using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class AdminController : ControllerBase
    {
        private IConfiguration _config;
        private IAdminServices _Services;
        public AdminController(IConfiguration config, IAdminServices service)
        {
            _config = config;
            _Services = service;
        }
        [HttpPost]
        public async Task<List<MoviesDto>> AddNewmovies(MoviesDto movies)
        {

           
            var data=await _Services.AddNewMovie(movies).ConfigureAwait(false);
            if (data != null)
            {
                return data.ToList();
            }
            else { 
            return data.ToList();
            }

        }
        [HttpGet]
        public async Task<List<GenreDto>> GetAllGenre()
        {
            var data = await _Services.GetListofGenre();
            return data.ToList();
        }
        [HttpPost]
        public async Task<List<ActorDto>> AddNewActor(ActorDto act)
        {
            var data = await _Services.AddNewActor(act);
            if (data != null) {

                return data.ToList();

            }
            else {
                return data.ToList();
            }
            
        }

    }
}
