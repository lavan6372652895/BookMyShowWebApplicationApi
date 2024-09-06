using BookMyShowWebApplicationServices.Interface.Admin;
using BookMyShowWebApplicationServices.Interface.ICommonMethods;
using BookMyShowWebApplicationServices.Interface.IHome;
using BookMyShowWebApplicationServices.Interface.Imail;
using BookMyShowWebApplicationServices.Interface.Theater;
using BookMyShowWebApplicationServices.Interface.Users;
using BookMyShowWebApplicationServices.Services.Admin;
using BookMyShowWebApplicationServices.Services.CommonMethods;
using BookMyShowWebApplicationServices.Services.Home;
using BookMyShowWebApplicationServices.Services.Mail;
using BookMyShowWebApplicationServices.Services.Theater;
using BookMyShowWebApplicationServices.Services.Users;

namespace BookMyShowWebApplicationServices
{
    public static class ServicesRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>()
            {
                { typeof(IHomenterface), typeof(HomeServices) },
                { typeof(IAdminServices), typeof(AdminServices) },
                {typeof(IUserServices),typeof(UserServices) },
                {typeof(ITheaterManage) ,typeof(TheaterManage)},
                {typeof(Imail),typeof(Mail) },
                {typeof(ICommonMethods),typeof(CommonMethods) },
            };
            return dictionary;
        }
    }
}
