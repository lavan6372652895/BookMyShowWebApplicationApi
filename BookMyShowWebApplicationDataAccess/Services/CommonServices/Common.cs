using AutoMapper;
using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo;
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

        //public async Task<List<RoleDto>> GetRoles()
        //{
        //    //RoleDto data =new();
        //   var result = await QueryMultipleAsync<RoleDto>(Storeprocedure.Common.ListofRoles, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        //    var data = result.Read<RoleDto>().ToList();
        //    return data;
        //}
        public async Task<List<RoleDto>> GetRoles()
        {
            try
            {
                var result = await QueryAsync<RoleDto>(Storeprocedure.Common.ListofRoles, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                var roles = result.ToList();
                //await CloseConnAsync().ConfigureAwait(false);
                return roles;  
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                //_logger.LogError(ex, "An error occurred while fetching roles.");

                // Handle or rethrow the exception as necessary
                throw ex;
            }
        }

        public async Task<UserDto> SingleUser(string username)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@Email", username);
            var data = await QueryFirstOrDefaultAsync<UserDto>(Storeprocedure.Common.Singleuserdata, parametar,commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return data;
        }
    }
}
