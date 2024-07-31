using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class UserDto
    {
        public int useridId { get; set; }
        public string? FullName { get; set;}
        public string? UserName { get; set;}
        public string? phonenumber { get; set;}
        public string? passwords { get; set; }
        public bool IsActive { get; set; }
        public string? Role {  get; set; }
    }
}
