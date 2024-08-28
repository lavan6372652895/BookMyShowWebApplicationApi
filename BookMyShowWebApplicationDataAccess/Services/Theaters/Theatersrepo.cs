using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.Theaters;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.config;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Services.Theaters
{
    public class Theatersrepo : BaseRepository,Itheatersrepo
    {
        public IConfiguration configuration;
        public Theatersrepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<List<ScreenDto>>AddNewScren(ScreenDto screen)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@Code",screen.code);
            parametar.Add("@Levels",screen.levels);
            parametar.Add("@SeatsPerLevel",screen.seatsPerLevel);
            parametar.Add("@TheatreID", screen.theatreID);
            parametar.Add("@SeatPrice", screen.seatPrice);
            var data = await QueryAsync<ScreenDto>(Storeprocedure.Theatersowner.AddNewScreen,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
          
        }

        public async Task<List<TheatersDto>> AddNewTheater(TheatersDto Theater)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@name",Theater.Name);
            parametar.Add("@Location", Theater.Location);
            parametar.Add("@Phone",Theater.Phone);
            parametar.Add("@stateid",Theater.stateid);
            parametar.Add("@CityName",Theater.CityName);
            parametar.Add("@Email",Theater.email);
            var data = await QueryAsync<TheatersDto>(Storeprocedure.Theatersowner.AddtheTheaters,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();
        }

        public async Task<List<Showtime>> AddShow(Showtime show)
        {
            var parametar = new DynamicParameters();
            parametar.Add("@MovieID",show.MovieID);
            parametar.Add("@ShowDate",show.ShowDate);
            parametar.Add("@showtime", show.ShowTime);
            parametar.Add("@screenid", show.ScreenId);
            var data = await QueryAsync<Showtime>(Storeprocedure.Theatersowner.AddShowTimeing,parametar,commandType:CommandType.StoredProcedure).ConfigureAwait(false);
            return data.ToList();  
        }

        public async Task<List<TheatersDto>> GetTheaterWithScreens()
        {
            var data = await QueryAsync<dynamic>(Storeprocedure.Theatersowner.GetTheaterswithScreen, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            List<TheatersDto> theaterList = new List<TheatersDto>();

            foreach (var item in data)
            {
                TheatersDto theater = new TheatersDto
                {
                    TheaterID = item.TheaterID,
                    Name = item.Name,
                    Location = item.Location,
                    Phone = item.Phone,
                    stateid = item.stateid,
                    CityName = item.CityName,
                    ownersid = item.ownersid,
                    email = item.email,
                    screen = JsonConvert.DeserializeObject<List<ScreenDto>>(item.screen)
                };

                theaterList.Add(theater);
            }

            return theaterList;
        }


    }
}
