using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo;
using BookMyShowWebApplicationModal;
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

namespace BookMyShowWebApplicationDataAccess.Services.CommonServices
{
    public class Common : BaseRepository,ICommon
    {
        public IConfiguration configuration;
        public Common(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<List<ActorDto>> GetActors()
        {
            var data = await QueryAsync<ActorDto>(Storeprocedure.Common.ListofActors,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }

        public async Task<List<Citydto>> GetCitys()
        {
            var data = await QueryAsync < Citydto >(Storeprocedure.Common.Listofcities,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var data = await QueryAsync<RoleDto>(Storeprocedure.Common.ListofRoles,commandType:CommandType.StoredProcedure).ConfigureAwait(false); 
            return data.ToList();  
        }
    }
}
