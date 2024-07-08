using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IConfiguration _config;
        public IHomenterface _serivices;
        public HomeController(IConfiguration config, IHomenterface serivices)
        {
            _config = config;
            _serivices = serivices;
        }


        [HttpGet]
        public async Task<List<RoleDto>> GetRoles()
        {
            var data = await _serivices.GetRoles();
            return data.ToList();
        }
        [HttpGet]
        public async Task<List<ActorDto>> GetActorlist()
        {
            var data = await _serivices.GetActors();
            return data.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Ensure _services.LoginUser is awaited if it is an asynchronous method
            var data = await _serivices.LoginUser(username, password);

            if (data == "Authenticated")
            {
                var token = GenerateJSONWebToken(username);
                return Ok(new JwtTokenmodal { token = token });
            }

            return Unauthorized(data); // Return an unauthorized response if not authenticated
        }

        private string GenerateJSONWebToken(string userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Name, userInfo),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64) // Issued At
          };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                //notBefore: DateTime.Now,
                //expires: DateTime.Now.AddMinutes(45),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}