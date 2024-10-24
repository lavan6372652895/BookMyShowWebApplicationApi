using BookMyShowWebApplicationDataAccess.InterFaces.Eventorganization;
using BookMyShowWebApplicationModal.organization;
using BookMyShowWebApplicationServices.Interface.Eventorganization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Eventorganization
{
    public class EventorganizationServices : IEventorganizationServices
    {
        public IEventorganizationRepo _repo;
        public EventorganizationServices(IEventorganizationRepo repo)
        {
            _repo = repo;
        }

        public async Task<EventorganizationDto> AddOrUpdateEventorganization(EventorganizationDto eventorganization)
        {
            var data = await _repo.AddOrUpdateEventorganization(eventorganization).ConfigureAwait(false);
            return data;
        }

        public async Task<EventDto> AddOrUpDateEvents(EventDto eventdto)
        {
          var data = await _repo.AddOrUpDateEvents(eventdto).ConfigureAwait(false);
            return data;
        }
    }
}
