using BookMyShowWebApplication.backGroundTask;
using BookMyShowWebApplication.Hub;
using BookMyShowWebApplication.Signalr;
using BookMyShowWebApplicationDataAccess;
using BookMyShowWebApplicationServices;
using Microsoft.AspNetCore.Hosting;

namespace BookMyShowWebApplication
{
    public static class RegisterServices
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

          
            services.AddSignalR();
            // services.AddSingleton<IMessageHubClient, MessageHub>();
            //services.AddHostedService<BackgroundServices>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddMvc();
            services.AddHttpContextAccessor();
        }
    }
}
