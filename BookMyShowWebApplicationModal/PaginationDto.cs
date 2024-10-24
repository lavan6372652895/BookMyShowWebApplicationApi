using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace BookMyShowWebApplicationModal
{
    public class PaginationDto<T>
    {
        public int pagenumbers {  get; set; }
        public int noofpages {  get; set; }
        public string shoring {  get; set; }
        public string serchvalue {  get; set; }
    }
}


