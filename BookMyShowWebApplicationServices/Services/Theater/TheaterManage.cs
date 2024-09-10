using BookMyShowWebApplicationDataAccess.InterFaces.Theaters;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Users;
using BookMyShowWebApplicationServices.Interface.Theater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Theater
{
    public class TheaterManage : ITheaterManage
    {
        private readonly Itheatersrepo _theatersrepo;
        public TheaterManage(Itheatersrepo theatersrepo) { 

            _theatersrepo = theatersrepo;
        }

        public async Task<List<ScreenDto>> AddNewScren(ScreenDto screen)
        {
            var data =await _theatersrepo.AddNewScren(screen);
            return data;
        }

        public async Task<List<TheatersDto>> AddNewTheater(TheatersDto Theater)
        {
           var data = await _theatersrepo.AddNewTheater(Theater);
            return data;
        }

        public async Task<List<Showtime>> AddShow(Showtime show)
        {
            var data = await _theatersrepo.AddShow(show);
            return data;
        }

        public async Task<List<TheatersDto>> GetTheaterWithScreens()
        {
           var data = await _theatersrepo.GetTheaterWithScreens();
            return data;
        }

        public async Task<List<ListofMovieTheaterscs>> moviesListOfTheaterList(int movieid, int cityid)
        {
           var data= await _theatersrepo.moviesListOfTheaterList(movieid, cityid);
            return data;
        }
    }
}
