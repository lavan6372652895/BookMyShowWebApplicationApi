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
        public string? SeatNumbers {  get; set; }
        public decimal Totalamount {  get; set; }
        public decimal TicketAmount { get; set; }
        public decimal GstAmount {  get; set; }
        public decimal Platformcharges { get; set; }
    }
}
