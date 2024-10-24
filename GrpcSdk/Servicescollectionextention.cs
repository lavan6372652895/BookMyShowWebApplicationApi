using BookMyShowWebApplication.GrpcService;
using GrpcSdk.Grpc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcSdk
{
    public static class Servicescollectionextention
    {
        public static void AddServiceGrpc(this IServiceCollection services)
        {
            services.AddGrpcClient<Greeter.GreeterClient>(client =>
            {
                client.Address = new Uri("https://localhost:7157");
            });
            services.AddScoped<IGrpcServices, GrpcServices>();
        }
    }
}
