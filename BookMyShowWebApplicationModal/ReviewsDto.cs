using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
   public class ReviewsDto
    {
        public int Id { get; set; }
        public string? Review {  get; set; }
        public decimal Rating { get; set; }
        public int movieName {  get; set; } 
        public int Userid { get; set; }
    }
}
