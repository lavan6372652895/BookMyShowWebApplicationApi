using BookMyShowWebApplication.GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcSdk.Grpc
{
    public class GrpcServices: IGrpcServices
    {
        private readonly Greeter.GreeterClient greeterClient;
        public GrpcServices(Greeter.GreeterClient greeterClient)
        {
            this.greeterClient = greeterClient;
        }

        public async Task<string> SayHello(string message, CancellationToken cancellationtoken)
        {
            try
            {
                var result = await greeterClient.SayHelloAsync(new HelloRequest { Name = "lavan" },cancellationToken:cancellationtoken);
                return result.Message;
            }
            catch (Exception ex) { 
            return ex.Message;
            }
        }
    }
}
