using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationDataAccess.InterFaces.Eventorganization;
using BookMyShowWebApplicationModal.config;
using BookMyShowWebApplicationModal.organization;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.Services.Eventorganization
{
    public class EventorganizationRepo : BaseRepository, IEventorganizationRepo
    {
        private readonly IConfiguration configuration;
        public EventorganizationRepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            configuration = config;
        }

        public async Task<EventorganizationDto> AddOrUpdateEventorganization(EventorganizationDto eventorganization)
        {
            EventorganizationDto Result = new EventorganizationDto();
            try
            {
                var parametar = new DynamicParameters();
                parametar.Add("@ID", eventorganization.Id);
                parametar.Add("@organizationname", eventorganization.organizationname);
                parametar.Add("@orglogo", eventorganization.orglogo);
                parametar.Add("@email", eventorganization.Email);
                parametar.Add("@Password", eventorganization.Password);
                parametar.Add("@orgAddress", eventorganization.orgAddress);
                parametar.Add("@organizationdescription", eventorganization.organizationdescription);
                EventorganizationDto data = await QueryFirstOrDefaultAsync<EventorganizationDto>(Storeprocedure.Eventorganizations.addorUpdateEventOrganizations, parametar, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                Result = data;
            }
            catch (Exception ex) { 
            
            }
           return Result;
        

        }

        public async Task<EventDto> AddOrUpDateEvents(EventDto eventdto)
        {
            EventDto result = new EventDto();
            try
            {
                var parametar = new DynamicParameters();
                parametar.Add("@Eventid", eventdto.Eventid);
                parametar.Add("@Eventtype", eventdto.Eventtype);
                parametar.Add("@Startdate", eventdto.Startdate);
                parametar.Add("@Enddtae", eventdto.Enddate);
                parametar.Add("@duration", eventdto.duration);
                parametar.Add("@EventAddress", eventdto.EventAddress);
                parametar.Add("@Eventcontact", eventdto.Eventcontact);
                parametar.Add("@Eventdesc", eventdto.Eventdesc);
                parametar.Add("@Orgid", eventdto.Orgid);
                parametar.Add("@BookingsStartdate", eventdto.BookingsStartdate);
                result = await QueryFirstOrDefaultAsync<EventDto>(Storeprocedure.Eventorganizations.addOrUpDateEvent, parametar, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex) { 
            return result;
            }
        }
    }
}
