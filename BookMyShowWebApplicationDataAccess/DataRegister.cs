using AutoMapper;
using BookMyShowWebApplicationDataAccess.InterFaces;
using BookMyShowWebApplicationDataAccess.InterFaces.Admin;
using BookMyShowWebApplicationDataAccess.InterFaces.CommonRepo;
using BookMyShowWebApplicationDataAccess.InterFaces.Email;
using BookMyShowWebApplicationDataAccess.InterFaces.Eventorganization;
using BookMyShowWebApplicationDataAccess.InterFaces.Theaters;
using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using BookMyShowWebApplicationDataAccess.Services;
using BookMyShowWebApplicationDataAccess.Services.Admin;
using BookMyShowWebApplicationDataAccess.Services.CommonServices;
using BookMyShowWebApplicationDataAccess.Services.Email;
using BookMyShowWebApplicationDataAccess.Services.Eventorganization;
using BookMyShowWebApplicationDataAccess.Services.Theaters;
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
            var dictionary = new Dictionary<Type, Type>()
            {
             {typeof(IBaseRepository),typeof(BaseRepository) },
             { typeof(IHome), typeof(HomeClass) },
             {typeof(IAdmin),typeof(Admins) },
             {typeof(ICommon),typeof(Common) },
             {typeof(IuserRepo),typeof(UserRepo) },
             {typeof(Itheatersrepo),typeof(Theatersrepo) },
             {typeof(Iemail),typeof(Email) },
             {typeof(IEventorganizationRepo),typeof(EventorganizationRepo) }
           
            };
            return dictionary;
        }
    }
}
