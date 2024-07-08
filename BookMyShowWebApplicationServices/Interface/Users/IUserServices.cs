using BookMyShowWebApplicationModal.Admin;
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
    }
}
