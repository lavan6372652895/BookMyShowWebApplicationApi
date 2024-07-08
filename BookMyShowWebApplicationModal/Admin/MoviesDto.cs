using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMyShowWebApplicationModal.Admin
{
    public class MoviesDto
    {
        public int MovieID { get; set; }
        
        public string? Title { get; set; }
        public string? profilepic { get; set; }
        public int Genre { get; set; }
        public int Duration { get; set; }
        public int Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Rating { get; set; }
        public string? Moviecast { get; set; }
        public string moviename {  get; set; }
        public string posterpic { get; set; }
    }
}
