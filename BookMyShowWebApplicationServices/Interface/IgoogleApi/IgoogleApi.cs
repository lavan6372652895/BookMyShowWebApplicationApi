using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyShowWebApplicationModal;
namespace BookMyShowWebApplicationServices.Interface.IgoogleApi
{
    public interface IgoogleApi
    {
        Task GetAddressAsync(string address);

    }
}
