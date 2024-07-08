using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using BookMyShowWebApplicationModal.Admin;
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
        public Task<List<MoviesDto>> MoviesList()
        {
           var data =_service.MoviesList();
            return data;
        }
    }
}
