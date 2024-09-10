using BookMyShowWebApplicationDataAccess.Queries;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        
    }
}
