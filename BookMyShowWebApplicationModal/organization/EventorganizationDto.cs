using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal.organization
{
    public class EventorganizationDto
    {
     public int Id { get; set; }
     public string? organizationname { get; set; }
     public string orglogo { get; set; }
     public string? Email {  get; set; }
     public string? Password {  get; set; }
     public string? orgAddress {  get; set; }   
     public string? organizationdescription { get; set; }


    }
    public class EventTicketCategory {
     public int CategoryId { get; set; }
     public string? Categorytype { get; set; }
    
    }


}
