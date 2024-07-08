using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Services.Users
{
    public class UserRepo : BaseRepository, IuserRepo
    {
        public IConfiguration configuration;
        public UserRepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<List<MoviesDto>> MoviesList()
        {
           var data = await QueryAsync<MoviesDto>(Storeprocedure.users.ListOfMovies,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();   
        }
    }
}
