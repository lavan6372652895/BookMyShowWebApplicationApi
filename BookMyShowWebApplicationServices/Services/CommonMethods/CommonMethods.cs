using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _config;
        public CommonMethods(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(UserDto userInfo, string role)
        {
            if (userInfo == null)
                throw new ArgumentNullException(nameof(userInfo), "User information must not be null.");

            var key = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT Key is not configured.");

            var issuer = _config["Jwt:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) ?? throw new ApplicationException("JWT key is not configured.");
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Ensure the properties of userInfo are not null
            var userName = userInfo.UserName ?? string.Empty;
            var userId = userInfo.userid.ToString();
            var userRole = userInfo.Roles ?? string.Empty;
            var phoneNumber = userInfo.phonenumber ?? string.Empty;
            var fullName = userInfo.FullName ?? string.Empty;
            var expiration = DateTime.UtcNow.AddMinutes(45);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier
                   new Claim(ClaimTypes.Role, role), // User role
                   new Claim(ClaimTypes.NameIdentifier, userName), // User identifier
                   new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),         ClaimValueTypes.Integer64), // Issued at
                   new Claim("_useridId", userId), // Custom claims
                   new Claim("_Role", userRole),
                   new Claim("phonenumber", phoneNumber),
                   new Claim("FullName", fullName),
                   new Claim("UserName", userName),

                }),
                Expires = expiration,
                Issuer = issuer, // Issuer
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

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
