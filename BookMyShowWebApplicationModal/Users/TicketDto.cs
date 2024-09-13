using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal.Users
{
    public class TicketDto
    {
        public  int BookingId { get; set; }
        public decimal Totalamount {  get; set; }
        public string? Moviename {  get; set; }
        public  string? posterpic {  get; set; }
        public string? Languagename {  get; set; }
        public DateTime showdate {  get; set; }
        public TimeSpan ShowTime { get; set; }
        public int numberofseats {  get; set; }
        public string? name {  get; set; }
        public string Code {  get; set; }
        public string? SeatNumbers {  get; set; }
        public DateTime BookingDateTime {  get; set; }

    }
}
