using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.IHome
{
    public interface IHomenterface
    {
        Task<string> LoginUser(Logindto logindto);
        Task<List<RoleDto>> GetRoles();
        Task<List<ActorDto>> GetActors();
        Task<List<Citydto>> GetCitys();
        Task<List<UserDto>> Adduser(UserDto user);
        Task<List<JwtTokenmodal>> AddToken(JwtTokenmodal token, string email);
        Task<int> ForgotPassword(Logindto logindto);
        Task<UserDto> SingleUser(string username);
    }
}
