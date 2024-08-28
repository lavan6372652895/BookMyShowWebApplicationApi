using BookMyShowWebApplicationDataAccess.InterFaces;
using BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo;
using BookMyShowWebApplicationDataAccess.Services;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public Task<List<JwtTokenmodal>> AddToken(JwtTokenmodal token, string email)
        {
            var data =_service.AddToken(token, email);
            return data;
        }

        public Task<List<UserDto>> Adduser(UserDto user)
        {
           var data =_service.Adduser(user);
            return data;
        }

        public Task<int> ForgotPassword(Logindto logindto)
        {
           var data =_service.ForgotPassword(logindto);
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

        public Task<List<RoleDto>> GetRoles()
        {
            var data = _common.GetRoles();
            return data;
        }

        public Task<string> LoginUser(Logindto logindto)
        {
           var data =_service.LoginUser(logindto);
            return data;
        }

        public Task<UserDto> SingleUser(string username)
        {
            var data =_common.SingleUser(username);
            return data;
        }
    }
}
