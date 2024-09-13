using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.Imail
{
    public interface Imail
    {
        Task<bool> sendEmail(string email,int bookingid=0);
    }
}
