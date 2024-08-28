using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.Admin;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.config;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Services.Admin
{
    public class Admins : BaseRepository, IAdmin
    {
        public readonly IConfiguration configuration;

        public Admins(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config) {
            configuration = config;
        }

        public async Task<List<ActorDto>> AddNewActor(ActorDto Act)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@id",Act.Id);
            parametar.Add("@Actorname",Act.Actorname);
            parametar.Add("@profilepic",Act.profilepic);
            parametar.Add("@gender",Act.gender);
            parametar.Add("@descriptions",Act.descriptions);
            parametar.Add("@role",Act.Roles);
            var data = await QueryFirstOrDefaultAsync<List<ActorDto>>(Storeprocedure.Admin.AddNewActor,parametar,commandType:CommandType.StoredProcedure)
              .ConfigureAwait(false);
            return data.ToList();
        }

        public async Task<List<MoviesDto>> AddNewMovie(MoviesDto movie)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@movieid",movie.MovieID);
            parametar.Add("@moviename", movie.Title);
            parametar.Add("@Genre",movie.Genre);
            parametar.Add("@posterpic", movie.profilepic);
            parametar.Add("@Moviecast",movie.Moviecast);
            parametar.Add("@ReleaseDate",movie.ReleaseDate);
            parametar.Add("@Duration",movie.Duration);
            parametar.Add("@languages",movie.Language);
            var data = await QueryFirstOrDefaultAsync<List<MoviesDto>>(Storeprocedure.Admin.NewMovieReg, parametar, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();

        }

        public async Task<List<GenreDto>> GetListofGenre()
        {
            var data = await QueryAsync<GenreDto>(Storeprocedure.Admin.ListofGenre, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }



    }
}
