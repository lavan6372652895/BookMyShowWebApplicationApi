using BookMyShowWebApplicationDataAccess.InterFaces;
using BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo;
using BookMyShowWebApplicationDataAccess.Services;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Home
{
    public class HomeServices : IHomenterface
    {
        public IHome _service;
        public ICommon _common;
        public HomeServices(IHome service, ICommon common)
        {
            _service = service;
            _common = common;
        }

        public Task<List<UserDto>> Adduser(UserDto user)
        {
           var data =_service.Adduser(user);
            return data;
        }

        public Task<List<ActorDto>> GetActors()
        {
            var data = _common.GetActors();
            return data;
        }

        public Task<List<Citydto>> GetCitys()
        {
           var data =_common.GetCitys();
            return data;
        }

        public  Task<List<RoleDto>> GetRoles()
        {
            var data = _common.GetRoles();
            return data;
        }

        public Task<string> LoginUser(string username, string password)
        {
           var data =_service.LoginUser(username, password);
            return data;
        }
    }
}
