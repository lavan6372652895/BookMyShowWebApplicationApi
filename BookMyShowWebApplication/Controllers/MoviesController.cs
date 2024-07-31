using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {

        
    }
}
