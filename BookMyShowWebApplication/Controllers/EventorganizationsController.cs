using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationModal.organization;
using BookMyShowWebApplicationServices.Interface.Eventorganization;
using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [AllowAnonymous]
    public class EventorganizationsController : ControllerBase
    {
        public IEventorganizationServices _services;
        public EventorganizationsController(IEventorganizationServices services) { 
            _services = services;
        }
        [HttpPost]
        public async Task<ApiPostResponse<EventorganizationDto>> AddNewOrganization(EventorganizationDto eventorganization) 
        {
            ApiPostResponse<EventorganizationDto> Response = new() { Data = new EventorganizationDto() };
            try
            {
                EventorganizationDto data =await _services.AddOrUpdateEventorganization(eventorganization).ConfigureAwait(false);
                Response.Success = true;
                Response.Statuscode = 200;
                Response.Data=data;
            }
            catch (Exception ex) { 
                Response.Success=false;
                Response.Message=ex.Message;
                Response.Statuscode=500;
            
            }
              return Response;

        }
        [HttpPost]
        public async Task<ApiPostResponse<EventDto>> AddOrUpDateEvents(EventDto eventDto)
        {
            ApiPostResponse<EventDto> response = new() { Data = new EventDto() };
            try
            {
                EventDto data = await _services.AddOrUpDateEvents(eventDto).ConfigureAwait(false);
                response.Success = true;
                response.Statuscode = 200;
                response.Data = data;
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
                response.Statuscode = 500;
            }
            return response;
        }
        
    }
}
