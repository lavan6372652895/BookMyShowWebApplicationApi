using BookMyShowWebApplicationServices.Interface.Admin;
using BookMyShowWebApplicationServices.Interface.IHome;
using BookMyShowWebApplicationServices.Interface.Users;
using BookMyShowWebApplicationServices.Services.Admin;
using BookMyShowWebApplicationServices.Services.Home;
using BookMyShowWebApplicationServices.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices
{
    public class ServicesRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>()
            {
                { typeof(IHomenterface), typeof(HomeServices) },
                { typeof(IAdminServices), typeof(AdminServices) },
                {typeof(IUserServices),typeof(UserServices) },
            };
            return dictionary;
        }
    }
}
