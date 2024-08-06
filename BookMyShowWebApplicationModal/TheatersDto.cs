using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class TheatersDto
    {
        public int TheaterID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public int stateid {  get; set; }
        public int CityName {  get; set; }
        public int ownersid {  get; set; }
        public string email {  get; set; }
        public List<ScreenDto> screen {  get; set; }
    }
}
