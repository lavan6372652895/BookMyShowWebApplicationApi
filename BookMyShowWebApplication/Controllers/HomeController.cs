using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IConfiguration _config;
        public IHomenterface _serivices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IConfiguration config, IHomenterface serivices, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _serivices = serivices;
            _httpContextAccessor = httpContextAccessor;
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var data = await _serivices.LoginUser(username, password);
            if (data == "Unauthenticated")
            {

                return Unauthorized(data); 
            }
            var token = GenerateJSONWebToken(username,data);
            return Ok(new JwtTokenmodal { token = token,
                starttime = DateTime.Now,
                endtime = DateTime.Now.AddMinutes(40)
            }); 
        }
        private string GenerateJSONWebToken(string userInfo, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
        new Claim(ClaimTypes.Role, role), 
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64), 
    };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"], // Issuer
                _config["Jwt:Audience"], // Audience (You should also include this in your token validation parameters)
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(45),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        public async Task<List<Citydto>> GetCitys()
        {
            var data = await _serivices.GetCitys();
            return data;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserDto user)
        {
            if (user != null)
            {
                var result = await _serivices.Adduser(user);
                if (result.Count > 0) {
                return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(user);
        }


        [HttpGet]
      
        public async Task<IActionResult> Window()
        {
            var user =  _httpContextAccessor.HttpContext.User;
            var userName = user.FindFirstValue(ClaimTypes.Name);
            if (userName != null)
            {
                return Ok(userName);
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}