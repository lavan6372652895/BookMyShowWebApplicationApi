using BookMyShowWebApplicationDataAccess.Queries;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using BookMyShowWebApplicationServices.Interface.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IUserServices _serivices;
        public MoviesController(IUserServices serivices)
        {
            _serivices = serivices;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<List<MoviesDto>> GetListofMovies()
        {

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
