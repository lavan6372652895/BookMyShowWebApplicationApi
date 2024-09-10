using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.config;
using BookMyShowWebApplicationModal.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Services.Users
{
    public class UserRepo : BaseRepository,IuserRepo
    {
        private readonly IConfiguration configuration;
        public UserRepo(IOptions<DataConfig> connectionString, IConfiguration? config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<string> Addseat(Bookingsdto[] booking)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@userid", booking[0].userid);
            parametar.Add("@showid",booking[0].Showid);
            parametar.Add("@Bookingid",booking[0].Bookingid);
            parametar.Add("@noofseats",booking[0].noofseats);
            parametar.Add("@seatno", booking[0].SeatNumbers);
            parametar.Add("@totalamount",booking[0].Totalamount);
            parametar.Add("@ticketAmount",booking[0].TicketAmount);
            parametar.Add("@GstAmount", booking[0].GstAmount);
            parametar.Add("@platformcharges",booking[0].Platformcharges);
            var data = await QueryFirstOrDefaultAsync<string>(Storeprocedure.Users.TicketBooking, parametar ,commandType: CommandType.StoredProcedure);
            return data.ToString(); 
        }

        public async Task<List<MoviesDto>> MoviesList()
        {
           var data = await QueryAsync<MoviesDto>(Storeprocedure.Users.ListOfMovies,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();   
        }

        

        public async Task<List<SeatesDto>> seatesList(int Showid)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@showtimeid", Showid);
            var data = await QueryAsync<SeatesDto>(Storeprocedure.Users.ListofSeats,parametar,commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }


    }
}
