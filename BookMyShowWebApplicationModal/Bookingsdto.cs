using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class Bookingsdto
    {
        public int userid {  get; set; }
        public int Showid {  get; set; }
        public int Bookingid {  get; set; }
        public int noofseats {  get; set; }
        public string SeatNumbers {  get; set; }
    }
}
