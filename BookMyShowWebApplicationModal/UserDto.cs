using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class UserDto
    {
        public int userid { get; set; }
        public string? FullName { get; set;}
        public string? UserName { get; set;}
        public string? phonenumber { get; set;}
        public string? passwords { get; set; }
        public bool IsActive { get; set; }
        public string? Roles {  get; set; }
    }
    public class Logindto
    {
        [Required]
        public string? email {get; set; }
        [Required]
        public string? password {get; set; }
    }
    
}
