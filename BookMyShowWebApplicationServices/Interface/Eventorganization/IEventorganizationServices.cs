using BookMyShowWebApplicationModal.organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.Eventorganization
{
    public interface IEventorganizationServices
    {
        Task<EventorganizationDto> AddOrUpdateEventorganization(EventorganizationDto eventorganization);
        Task<EventDto> AddOrUpDateEvents(EventDto eventdto);
    }
}
