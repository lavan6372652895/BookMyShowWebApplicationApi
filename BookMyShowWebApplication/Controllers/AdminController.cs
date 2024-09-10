using AutoMapper.Configuration.Annotations;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.Admin;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
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
   
        private readonly IAdminServices _Services;
        private readonly ICommonMethods _CommonMethods;
        public AdminController(IAdminServices service, ICommonMethods commonMethods)
        {
            
            _Services = service;
            _CommonMethods = commonMethods;
        }
        [HttpPost]
        public async Task<List<MoviesDto>> AddNewmovies(MoviesDto movies)
        {
          


            var data=await _Services.AddNewMovie(movies).ConfigureAwait(false);
            return data?.ToList() ?? new List<MoviesDto>();

        }
        [HttpGet]
        public async Task<List<GenreDto>> GetAllGenre()
        {

            string token = HttpContext.Request.Headers.Authorization.ToString();
            var user = _CommonMethods.GetUserTokenData(token);
            var data = await _Services.GetListofGenre();
            return data?.ToList() ?? new List<GenreDto>();
        }
        [HttpPost]
        public async Task<List<ActorDto>> AddNewActor(ActorDto act)
        {
            var data = await _Services.AddNewActor(act);
            return data?.ToList() ?? new List<ActorDto>();
        }

    }
}
