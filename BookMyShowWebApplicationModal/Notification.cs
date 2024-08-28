using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class Notificationdto
    {
        public int id {  get; set; }
        public string? title { get; set; }
        public string? Notificationmessage { get; set; }
        public string? fromnotification {  get; set; }
        public string? tonotification { get; set; }
        public DateTime? notificationdate { get; set; }
    }
}
