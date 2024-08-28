using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class ScreenDto
    {
        public int id { get; set; }

        public string code { get; set; }

        public int levels { get; set; }

        public int seatsPerLevel { get; set; }

        public int theatreID { get; set; }

        //public int MovieID { get; set; }

        public int seatPrice { get; set; }

        //public DateTime ShowTime { get; set; }
    }

    public class Showtime
    {
        public int ShowtimeID { get; set; }
        public int MovieID { get; set; }
        public DateTime ShowDate {  get; set; } 
        public DateTime ShowTime {  get; set; }
        public int ScreenId {  get; set; }

    }

}
