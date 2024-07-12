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
        Task<string> LoginUser(string username, string password);
        Task<List<RoleDto>> GetRoles();
        Task<List<ActorDto>> GetActors();
        Task<List<Citydto>> GetCitys();
    }
}
