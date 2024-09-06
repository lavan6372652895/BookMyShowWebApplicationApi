using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo
{
    public interface ICommon
    {
        Task<List<RoleDto>> GetRoles();
        Task<List<ActorDto>> GetActors();
        Task<List<Citydto>> GetCitys();
        Task<UserDto> SingleUser(string username);
      
    }
}
