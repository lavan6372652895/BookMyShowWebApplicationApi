using BookMyShowWebApplicationDataAccess;
using BookMyShowWebApplicationServices;

namespace BookMyShowWebApplication
{
    public class RegisterServices
    {
        public static void RegisterService(IServiceCollection services)
        {
            Configure(services, DataRegister.GetTypes());
            Configure(services, ServicesRegister.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> dictionary)
        {
            foreach (var type in dictionary)
            {
                services.AddScoped(type.Key, type.Value);
            }

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddMvc();
            services.AddHttpContextAccessor();
        }
    }
}
