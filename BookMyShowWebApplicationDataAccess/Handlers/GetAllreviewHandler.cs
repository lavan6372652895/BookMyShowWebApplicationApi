using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.Queries;
using BookMyShowWebApplicationModal;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Handlers
{
    public class GetAllreviewHandler : IRequestHandler<GetAllReviews, List<ReviewsDto>>
    {
        public readonly IBaseRepository Repository;
        public GetAllreviewHandler(IBaseRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<ReviewsDto>>Handle(GetAllReviews request, CancellationToken cancellationToken)
        {
            var data = await Repository.QueryAsync<ReviewsDto>(Storeprocedure.Movies.getAllReviews, commandType: CommandType.StoredProcedure);
       return data.ToList();    
        }
    }
    public class GetReviewByMovieidHandler : IRequestHandler<GetReviewByMovieid, List<ReviewsDto>>
    {
        public readonly IBaseRepository Repository;
        public GetReviewByMovieidHandler(IBaseRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<ReviewsDto>> Handle(GetReviewByMovieid request, CancellationToken cancellationToken)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@movieid", request.Id);
            var data = await Repository.QueryAsync<ReviewsDto>(Storeprocedure.Movies.GetReviewByMovieid, parametar, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }

        

    }

    public class AddorUpdateRewviewHandler : IRequestHandler<AddorUpdateRewview, List<ReviewsDto>>
    {

        public readonly IBaseRepository Repository;
        public AddorUpdateRewviewHandler(IBaseRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<ReviewsDto>> Handle(AddorUpdateRewview request, CancellationToken cancellationToken)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@id", request.Review.Id);
            parametar.Add("@review", request.Review.Review);
            parametar.Add("@rating", request.Review.Rating);
            parametar.Add("@movieName", request.Review.movieName);
            parametar.Add("@userid", request.Review.Userid);
            var data = await Repository.QueryAsync<ReviewsDto>(Storeprocedure.Movies.AddorUpdateRewview,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();

        }
    }
}
