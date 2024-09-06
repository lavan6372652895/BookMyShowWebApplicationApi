using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.CommonMethods
{
    public  class CommonMethods: ICommonMethods
    {
        public object GetUserTokenData(string jwtToken)
        {
            if (!string.IsNullOrEmpty(jwtToken))
            {
              
                jwtToken = Regex.Replace(jwtToken, "^Bearer\\s+", "", RegexOptions.IgnoreCase);

                var handler = new JwtSecurityTokenHandler();

                try
                {
                    // Read the token
                    JwtSecurityToken token =  handler.ReadToken(jwtToken) as JwtSecurityToken;

                    if (token != null)
                    {
                        // Check if the token is expired
                        if (token.ValidTo < DateTime.UtcNow)
                        {
                            // Token is expired, return an empty object or null
                            return new { Error = "Token expired" };
                        }

                        // Extract claims from the token
                        var claims = token.Claims.ToList();

                        // Optionally, convert claims to a dictionary or other format
                        var claimsDictionary = claims.ToDictionary(c => c.Type, c => c.Value);
                        return claimsDictionary; // Or any other structure you need
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions such as invalid tokens
                    return new { Error = $"Invalid token: {ex.Message}" };
                }
            }

            // Return an empty object or null if the token is null or empty
            return new { Error = "Token is null or empty" };
        }
    }
}
