using BookMyShowWebApplication.GrpcService;
using BookMyShowWebApplicationModal;
using Grpc.Core;

namespace BookMyShowWebApplication.GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
        //public override Task<GoodbyeResponse> Goodbye(HelloRequest request, ServerCallContext context)
        //{
        //    // Log the request for debugging purposes
        //    _logger.LogInformation("Received Goodbye request with Name: {Name}", request.Name);

        //    // Return a response with a goodbye message
        //    return Task.FromResult(new GoodbyeResponse
        //    {
        //        Greeting = "Goodbye " + request.Name
        //    });
        //}

    }
}
