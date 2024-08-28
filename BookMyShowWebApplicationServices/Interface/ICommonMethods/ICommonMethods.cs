using BookMyShowWebApplicationModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Interface.ICommonMethods
{
    public interface ICommonMethods
    {
        object GetUserTokenData(string jwtToken);
       
    }
}
