﻿using BookMyShowWebApplicationModal;
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
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
namespace BookMyShowWebApplication.Controllers
{
    [Route("BookMyShow/[controller]/[Action]")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        private readonly IHomenterface _serivices;
        private  ILogger<HomeController> _logger;
        private readonly ICommonMethods _commonMethods;
        public HomeController(IHomenterface serivices,ILogger<HomeController> logger,ICommonMethods commonmethods)
        {
          
            _serivices = serivices;
            _logger = logger;
            _commonMethods = commonmethods;
          
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
                    var token = _commonMethods.GenerateJSONWebToken(userdeatils, result);

                   
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
                     await _serivices.AddToken(jwtTokenModal, logindto.email ?? string.Empty).ConfigureAwait(false);
                   

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
        [Authorize]
        public IActionResult Logout()
        {
            try
            {  // Delete the authentication cookie
                HttpContext.Response.Cookies.Delete("accessToken");

                // Return a success response
                return Ok(new { success = true });
            }
            catch {
                return BadRequest(new { success = false });
            
            }
          
        
        }
              
        [AllowAnonymous]
        [HttpPost("chatGpt")]
        public async Task<IActionResult> ChatGpt(string inputvalue)
        {
            if (string.IsNullOrWhiteSpace(inputvalue))
            {
                return BadRequest("Input value cannot be empty.");
            }
            string apikey = "sk-npbDCeifwDJPIuvmFSWT5hgK1UJ9PwOGBcQguIEfz3T3BlbkFJeFtXZUmlfcU9uz6CNJN0BxvgtyLODosDizenIeLjgA";
            OpenAIAPI _openai = new OpenAIAPI(apikey);
           try
        {
            var chatRequest = new OpenAI_API.Chat.ChatRequest
            {
                Model = "gpt-3.5-turbo-1106",
                Messages = new[] { new OpenAI_API.Chat.ChatMessage { TextContent = inputvalue } }
            };

            var response = await _openai.Chat.CreateChatCompletionAsync(chatRequest);

            if (response != null && response.Choices.Count > 0)
            {
                var result = response.Choices[0].Message;
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