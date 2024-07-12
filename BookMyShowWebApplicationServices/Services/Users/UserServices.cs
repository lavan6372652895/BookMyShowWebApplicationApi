using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.Users;
using BookMyShowWebApplicationServices.Interface.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Users
{
    public class UserServices : IUserServices
    {
        private IuserRepo _service;
        public UserServices(IuserRepo service) { 
            _service = service;
        }

        public Task<string> Addseat(Bookingsdto booking)
        {
           var data = _service.Addseat(booking);
            return data;
        }

        public Task<List<MoviesDto>> MoviesList()
        {
           var data =_service.MoviesList();
            return data;
        }

       

       public Task<List<ListofMovieTheaterscs>> moviesListOfTheaterList(int movieid, int cityid)
        {
           var data =_service.moviesListOfTheaterList(movieid, cityid);
            return data;
        }

        public Task<List<SeatesDto>> seatesList(int Showid)
        {
           var data =_service.seatesList(Showid);
            return data;
        }
    }
}
