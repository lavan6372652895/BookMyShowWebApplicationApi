using BookMyShowWebApplicationDataAccess.InterFaces;
using BookMyShowWebApplicationModal.config;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BookMyShowWebApplicationCommon.Helper;
using System.Data;
using BookMyShowWebApplicationModal;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookMyShowWebApplicationDataAccess.Services
{
    public class HomeClass : BaseRepository,IHome
    {
        private readonly IConfiguration configuration;

        public HomeClass(IOptions<DataConfig> connectionString, IConfiguration? config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<List<JwtTokenmodal>> AddToken(JwtTokenmodal token, string email)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@token",token.token);
            parametar.Add("@starttime", token.starttime);
            parametar.Add("@Endtime",token.endtime);
            parametar.Add("@Email", email);
            var data= await QueryAsync<JwtTokenmodal>(Storeprocedure.Common.AddToken,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();

        }

        public async Task<List<UserDto>> Adduser(UserDto user)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@fullname",user.FullName);
            parametar.Add("@username",user.UserName);
            parametar.Add("@phonenumber",user.phonenumber);
            parametar.Add("@password",user.passwords);
            parametar.Add("@Role",user.Roles);
            var data = await QueryAsync<UserDto>(Storeprocedure.Common.RegistrationSp,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }

        public async Task<int> ForgotPassword(Logindto logindto)
        {
           var parametar =new DynamicParameters();
            parametar.Add("@password",logindto.password);
            parametar.Add("@email",logindto.email);
            var data = await QueryAsync<int>(Storeprocedure.Common.Forgotpassword,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.First();
        }

        public async Task<string> LoginUser(Logindto logindto)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@UserName", logindto.email);
            parametar.Add("@password", logindto.password);
            var Data = await QueryFirstOrDefaultAsync<string>(Storeprocedure.Common.LoginSp, parametar, commandType: CommandType.StoredProcedure);
            return Data;
        }
    }
}
