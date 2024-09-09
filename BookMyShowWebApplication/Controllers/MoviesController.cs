using BookMyShowWebApplicationDataAccess.Queries;
using BookMyShowWebApplicationModal;
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
        private readonly IMediator _mediator;
        public MoviesController(IMediator mediator) { 
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<List<ReviewsDto>> GetAllReviews()
        {
            var query = new GetAllReviews();
            var result =await _mediator.Send(query);
            return result;
        }
        [HttpGet]
        public async Task<List<ReviewsDto>> GetReviewByMovieid(int movieid)
        {
            var query =new GetReviewByMovieid(movieid);
            var result = await _mediator.Send(query);   
            return result;
        }
        [HttpPost]

        public async Task<List<ReviewsDto>> AddorUpdateRewview(ReviewsDto rew)
        {
            var query = new AddorUpdateRewview(rew);
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
