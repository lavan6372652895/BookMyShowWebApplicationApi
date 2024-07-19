using BookMyShowWebApplicationDataAccess.InterFaces;
using BookMyShowWebApplicationDataAccess.InterFaces.Admin;
using BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo;
using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using BookMyShowWebApplicationDataAccess.Services;
using BookMyShowWebApplicationDataAccess.Services.Admin;
using BookMyShowWebApplicationDataAccess.Services.CommonServices;
using BookMyShowWebApplicationDataAccess.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess
{
    
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>() {
             { typeof(IHome), typeof(HomeClass) },
             {typeof(IAdmin),typeof(Admins) },
             {typeof(ICommon),typeof(Common) },
                {typeof(IuserRepo),typeof(UserRepo) }
    

    };
            return dictionary;
        }
    }
}
