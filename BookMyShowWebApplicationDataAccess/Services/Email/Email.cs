using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.Email;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.config;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Services.Email
{
    public class Email :BaseRepository, Iemail
    {
        private readonly IConfiguration configuration;
        public Email(IOptions<DataConfig> connectionString, IConfiguration? config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<List<OtpVerification>> addOtp(OtpVerification otp)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@otptime",otp.otptime);
            parametar.Add("@email",otp.Email);
            parametar.Add("@otp",otp.otp);
            var data = await QueryAsync<OtpVerification>(Storeprocedure.Email.AddOtp, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
    }
}
