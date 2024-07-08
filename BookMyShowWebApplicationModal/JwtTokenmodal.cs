using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class JwtTokenmodal
    {
        public string? token {  get; set; }
        public DateTime? starttime { get; set; }
        public DateTime? endtime { get; set; }
    }
}
