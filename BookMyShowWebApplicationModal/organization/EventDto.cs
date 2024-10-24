using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal.organization
{
    public class EventDto
    {
        public int Eventid { get; set; }
        public string? Eventtype { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int duration { get; set; }
        public string EventAddress { get; set; }
        public string Eventcontact { get; set; }
        public string Eventdesc { get; set; }
        public int Orgid {  get; set; }
        public DateTime BookingsStartdate {  get; set; }
    }

}
