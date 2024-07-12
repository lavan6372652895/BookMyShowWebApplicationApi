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
        public IConfiguration configuration;
        public UserRepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<string> Addseat(Bookingsdto booking)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@userid", booking.userid);
            parametar.Add("@showid", booking.Showid);
            parametar.Add("@Bookingid", booking.Bookingid);
            parametar.Add("@noofseats", booking.noofseats);
            parametar.Add("@seatno", booking.SeatNumbers);
            var data = await QueryFirstOrDefaultAsync<string>(Storeprocedure.Users.TicketBooking, parametar ,commandType: CommandType.StoredProcedure);
            return data.ToString(); 
        }

        public async Task<List<MoviesDto>> MoviesList()
        {
           var data = await QueryAsync<MoviesDto>(Storeprocedure.Users.ListOfMovies,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();   
        }

        public async Task<List<ListofMovieTheaterscs>> moviesListOfTheaterList(int movieid, int cityid)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@movieid",movieid);
            parametar.Add("@cityid", cityid);
            var data = await QueryAsync<ListofMovieTheaterscs>(Storeprocedure.Users.Theaterlist,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
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
