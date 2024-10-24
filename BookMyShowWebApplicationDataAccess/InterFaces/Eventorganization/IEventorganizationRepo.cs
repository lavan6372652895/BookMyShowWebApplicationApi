using BookMyShowWebApplicationModal.organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces.Eventorganization
{
    public interface IEventorganizationRepo
    {
        Task<EventorganizationDto> AddOrUpdateEventorganization(EventorganizationDto eventorganization);
        Task<EventDto> AddOrUpDateEvents(EventDto eventdto);
    }
}
