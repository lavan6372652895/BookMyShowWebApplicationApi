using BookMyShowWebApplicationDataAccess.InterFaces;
using BookMyShowWebApplicationModal.config;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BookMyShowWebApplicationCommon.Helper;
using System.Data;

namespace BookMyShowWebApplicationDataAccess.Services
{
    public class HomeClass : BaseRepository,IHome
    {
        private IConfiguration configuration;

        public HomeClass(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@UserName", username);
            parametar.Add("@password", password);
            var Data = await QueryFirstOrDefaultAsync<string>(Storeprocedure.Common.LoginSp, parametar, commandType: CommandType.StoredProcedure);
            return Data;
        }
    }
}
