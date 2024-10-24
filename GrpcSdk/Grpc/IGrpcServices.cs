using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcSdk.Grpc
{
    public interface IGrpcServices
    {
        Task<string>SayHello(string message,CancellationToken token);
    }
}
