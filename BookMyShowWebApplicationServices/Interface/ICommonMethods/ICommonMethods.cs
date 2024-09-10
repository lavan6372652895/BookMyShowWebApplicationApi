using BookMyShowWebApplicationModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.ICommonMethods
{
    public interface ICommonMethods
    {
        UserDto GetUserTokenData(string jwtToken);
        string GenerateJSONWebToken(UserDto userInfo, string role);


    }
}
