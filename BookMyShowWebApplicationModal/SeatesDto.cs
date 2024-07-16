using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class SeatesDto
    {
        public int SeatID { get; set; }
        public int ShowtimeID {  get; set; }
        public string SeatNumber {  get; set; }
        public Boolean IsBooked {  get; set; }
        public int levels { get; set; }
        public int ScreenId {  get; set; } 
        public TimeSpan ShowTime {  get; set; }
        public DateTime ShowDate { get; set; }
        public decimal Price {  get; set; }
    }
}
