using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class Maildata
    {
        public string? FromEmail { get; set; }
        public string? ToEmail { get; set; }
        public string? subject {  get; set; }
        public string? body { get; set; }
       // public maildto? maildto { get; set; }  
    }
    public class maildto
    {
        public string? SenderEmail { get; set; }
        public string? password { get; set; }
    }
}
