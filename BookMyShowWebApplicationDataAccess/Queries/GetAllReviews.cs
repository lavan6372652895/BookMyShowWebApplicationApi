using BookMyShowWebApplicationModal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Queries
{
    public class GetAllReviews:IRequest<List<ReviewsDto>>
    {
    }
    public class GetReviewByMovieid : IRequest<List<ReviewsDto>>
    {
        public int Id { get; }
        public GetReviewByMovieid(int id)
        {
            Id = id;
        }
    }
    public class AddorUpdateRewview : IRequest<List<ReviewsDto>>
    {
        public ReviewsDto? Review { get; }
        public AddorUpdateRewview(ReviewsDto? review)
        {
            Review = review;
        }
    }
}
