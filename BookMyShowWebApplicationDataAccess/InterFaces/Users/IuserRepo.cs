using BookMyShowWebApplicationModal.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces.Users
{
    public interface IuserRepo
    {
        Task<List<MoviesDto>> MoviesList();
    }
}
