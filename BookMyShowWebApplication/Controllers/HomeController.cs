using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using BookMyShowWebApplicationCommon.Helper;
using OpenAI_API;

using OpenAI_API.Chat;
namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        public IConfiguration _config;
        public IHomenterface _serivices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(IConfiguration config, IHomenterface serivices, IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
        {
            _config = config;
            _serivices = serivices;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
          
        }
        [HttpGet]
        public async Task<ApiResponse<RoleDto>> GetRoles()
        {
            var response = new ApiResponse<RoleDto>();
            try
            {
                var data = await _serivices.GetRoles().ConfigureAwait(false);
                if (data != null)
                {
                    response.Statuscode = 200;
                    response.Data = (IList<RoleDto>?)data;
                    response.Message = "OK";
                    response.Success = true;
                }
                else
                {
                    response.Statuscode = 404; // Not Found if no data is found
                    response.Message = "No roles found";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                // Log exception details here if needed
                response.Statuscode = 500;
                response.Message = "An error occurred:" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        [HttpGet]
        public async Task<List<ActorDto>> GetActorlist()
        {
            var data = await _serivices.GetActors();
            return data?.ToList() ?? new List<ActorDto>();
        }

        [HttpPost]
       
        public async Task<ApiPostResponse<JwtTokenmodal>> Login(Logindto logindto)
        {
            var response = new ApiPostResponse<JwtTokenmodal>();
            var jwtTokenModal = new JwtTokenmodal();
            try
            {
                // Call the service method to validate user login
                var result = await _serivices.LoginUser(logindto).ConfigureAwait(false);
                // Check the result of the login attempt
                if (result == "Unauthenticated")
                {
                    response.Statuscode = 401;
                    response.Message = "Password is incorrect";
                    response.Success=false;
                    response.Data=jwtTokenModal;
                }
                else if (result == "Passwordexpired please change the password")
                {
                    response.Statuscode = 201;
                    response.Message = "Passwordexpired";
                    response.Success = false;
                    response.Data = jwtTokenModal;
                }
                else
                {
                    // Generate JWT token if authentication is successful
                    var userdeatils = await _serivices.SingleUser(logindto.email ?? string.Empty);
                    var token = GenerateJSONWebToken(userdeatils, result);

                   
                     jwtTokenModal = new JwtTokenmodal
                    {
                        token = token,
                        starttime = DateTime.Now,
                        endtime = DateTime.Now.AddDays(1)
                    };
                    // Optionally save the token in the database or cache
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true, // Makes the cookie inaccessible to JavaScript
                        Secure = true,   // Ensures the cookie is only sent over HTTPS
                        SameSite = SameSiteMode.Strict, // Helps to prevent CSRF attacks
                        Expires = DateTime.Now.AddDays(1), // Cookie expiration
                        
                    };

                    HttpContext.Response.Cookies.Append("accessToken", token, cookieOptions);
                    var result1=   await _serivices.AddToken(jwtTokenModal, logindto.email ?? string.Empty).ConfigureAwait(false);
                   

                    response.Data = jwtTokenModal;
                    response.Statuscode = 200;
                    response.Message = "Login successful";
                    response.Data = jwtTokenModal;
                    response.Success = true;    
                }
            }
            catch (Exception ex)
            {
                response.Statuscode = 500; // Internal Server Error
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        
        private string GenerateJSONWebToken(UserDto userInfo, string role)
        {
            if (userInfo == null)
                throw new ArgumentNullException(nameof(userInfo), "User information must not be null.");

            var key = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT Key is not configured.");

            var issuer = _config["Jwt:Issuer"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
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
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64), // Issued at
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



        [HttpGet]
        [Authorize(Roles = "user")]
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiPostResponse<Logindto>> ForgotPasswordAsync(Logindto login)
        {
            var response = new ApiPostResponse<Logindto>();

            try
            {
                // Await the asynchronous ForgotPassword method
                var result = await _serivices.ForgotPassword(login).ConfigureAwait(false);

                // Assuming result is a boolean indicating success
                if (result==200)
                {
                    response.Success = true;
                    response.Message = "Password will be changed successfully.";
                    response.Data = login; // Assuming you want to return the login info
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to change password."; // You might want a different message based on the result
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                response.Success = false;
                response.Message = ex+"An error occurred while changing the password.";
                // Optionally, log the exception or include more details in the response
            }

            return response;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            // Delete the authentication cookie
            HttpContext.Response.Cookies.Delete("accessToken");

            // Return a success response
            return Ok(new { success = true });
        }
              
        [AllowAnonymous]
        [HttpPost("chatGpt")]
        public async Task<IActionResult> ChatGpt(string inputvalue)
        {
            if (string.IsNullOrWhiteSpace(inputvalue))
            {
                return BadRequest("Input value cannot be empty.");
            }
            string apikey = "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";
            OpenAIAPI _openai = new OpenAIAPI(apikey);
           try
        {
            var chatRequest = new OpenAI_API.Chat.ChatRequest
            {
                Model = "gpt-3.5-turbo",
                Messages = new[] { new OpenAI_API.Chat.ChatMessage { TextContent = inputvalue } }
            };

            var response = await _openai.Chat.CreateChatCompletionAsync(chatRequest);

            if (response != null && response.Choices.Count > 0)
            {
                var result = response.Choices[0].Message.Content;
                return Ok(result);
            }

            return BadRequest("No completions received from the API.");
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("insufficient_quota"))
        {
            // Handle quota errors
            return StatusCode(429, "API quota exceeded. Please check your usage and billing details.");
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    }
}