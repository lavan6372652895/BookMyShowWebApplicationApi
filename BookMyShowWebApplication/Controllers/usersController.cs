using BookMyShowWebApplication.Signalr;
using BookMyShowWebApplicationDataAccess.InterFaces.Email;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationModal.Admin;
using BookMyShowWebApplicationModal.Users;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using BookMyShowWebApplicationServices.Interface.IHome;
using BookMyShowWebApplicationServices.Interface.Imail;
using BookMyShowWebApplicationServices.Interface.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserServices _serivices;
        private  IHubContext<MessageHub> _hubContext;
        private readonly ICommonMethods _commonmethod;
        private readonly Imail _email;
        public UsersController( IUserServices serivices, IHubContext<MessageHub> hubContext,ICommonMethods commonmethod, Imail email)
        {
            _serivices = serivices;
            _hubContext = hubContext;
            _commonmethod = commonmethod;
            _email=email;
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin,user")]
        public Task<List<SeatesDto>> GetListofSeates(int Showid)
        { 
            var data =_serivices.seatesList(Showid);
            if(data.Result.Count > 0)
            {
                return Task.FromResult(data.Result);
            }
            else { return data; }
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin,user")]
        public  async Task<string> AddBooking(int bookingid)
        {
            string result = string.Empty;
            try
            {
                //string jwt = HttpContext.Request.Headers.Authorization.ToString();
                string jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0NGMyYzI0Zi1kY2M5LTRiZDQtYTBiOS04NWU3NTM1MmRjYjkiLCJyb2xlIjoiVGhlYXRlcnMiLCJuYW1laWQiOiIxOTAxMDExMjAwMTZAY3V0bS5hYy5pbiIsImlhdCI6MTcyNjExNjAwNiwiX3VzZXJpZElkIjoiNyIsIl9Sb2xlIjoiVGhlYXRlcnMiLCJwaG9uZW51bWJlciI6Ijk2Mzg1Mjc0MTAiLCJGdWxsTmFtZSI6IkxhdmFuIiwiVXNlck5hbWUiOiIxOTAxMDExMjAwMTZAY3V0bS5hYy5pbiIsIm5iZiI6MTcyNjExNjAwNiwiZXhwIjoxNzI2MTE4NzA2LCJpc3MiOiJCb29rbXlzaG93d2ViYXBsbGljYXRpb24uY29tIn0.SYNZsO29kRXO6BHGyLLIq9ZGBOy98EJ391UlG0AsBe4";
                UserDto user = _commonmethod.GetUserTokenData(jwt);
                //var data = await _serivices.Addseat(bookingid);
                if (user.UserName != null && bookingid !=0) {
                  await _email.sendEmail(user.UserName,bookingid);
                }
                // await _hubContext.Clients.All.UpdateSeatAvailability();
                return result;
            }
            catch
            {
                return result;
            }
        }

    }
}
