using BookMyShowWebApplicationModal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces
{
    public interface IHome
    {
        Task<string>LoginUser(string username, string password);
        Task<List<UserDto>> Adduser(UserDto user);
        Task<List<JwtTokenmodal>> AddToken(JwtTokenmodal token, string email);
    }
}
