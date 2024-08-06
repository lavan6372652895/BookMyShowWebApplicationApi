using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Theater;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public TheaterOwnersController(IConfiguration config,ITheaterManage theater)
        {
            _config = config; 
            _theater= theater;
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
        [AllowAnonymous]
        public async Task<List<TheatersDto>> GetTheaterWithScreens()
        {
            var data =await _theater.GetTheaterWithScreens();
            return data;
        }
    }
}
