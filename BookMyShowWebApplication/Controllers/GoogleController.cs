using BookMyShowWebApplicationServices.Interface.IgoogleApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class GoogleController : ControllerBase
    {
        private readonly IgoogleApi _google;
        public GoogleController(IgoogleApi google) 
        { 
            _google = google;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> getAddress(string address)
        {
            await _google.GetAddressAsync(address);
            return Ok();
        }

    }
}
