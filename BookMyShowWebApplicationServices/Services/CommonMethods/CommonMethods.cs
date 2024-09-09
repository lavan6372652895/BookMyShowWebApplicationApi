using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.CommonMethods
{
    public  class CommonMethods: ICommonMethods
    {
        public UserDto GetUserTokenData(string jwtToken)
        {
            UserDto user = new UserDto();
            if (!string.IsNullOrEmpty(jwtToken)) {

                jwtToken = Regex.Replace(jwtToken, "Bearer ", "", RegexOptions.IgnoreCase);
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken? token = handler.ReadToken(jwtToken) as JwtSecurityToken;
                if (token == null)
                {
                    return new UserDto();
                }
                else { 
                var claims = token.Claims.ToList();
                    user.userid= Convert.ToInt32(claims.First(x=>x.Type== "_useridId").Value);
                    user.Roles=claims.First(x=>x.Type== "_Role").Value;
                    user.phonenumber = claims.First(x => x.Type == "phonenumber").Value;
                    user.FullName = claims.First(x => x.Type == "FullName").Value;
                    user.UserName = claims.First(x => x.Type == "UserName").Value;
                        
                }
            }
            return user;
        }
    }
}
