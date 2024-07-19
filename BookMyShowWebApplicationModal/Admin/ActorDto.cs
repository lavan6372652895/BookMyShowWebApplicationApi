using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal.Admin
{
    public class ActorDto
    {
        public int Id { get; set; }
        public string? Actorname { get; set; }
        [Required]
        public string? profilepic { get; set; }
        [Required]
        public string? gender { get; set; }
        [Required]
        public string? descriptions {  get; set; }
        [Required]
        public int Roles { get; set; }
   
    }   
}
