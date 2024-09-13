using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.Users
{
    public interface IUserServices
    {
        Task<List<MoviesDto>> MoviesList();
       
        Task<List<SeatesDto>> seatesList(int Showid);
        Task<Bookingsdto> Addseat(Bookingsdto[] booking);
    }
}
