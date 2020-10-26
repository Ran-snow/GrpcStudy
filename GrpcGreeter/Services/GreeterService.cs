using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

using Greet.V1;

using Grpc.Core;

using Microsoft.Extensions.Logging;

namespace GrpcGreeter
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
            HelloReply helloReply = new HelloReply();

            //string
            helloReply.Message = "Hello " + request.Name;
            //google.protobuf.Timestamp
            helloReply.Start = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
            ////google.protobuf.Int32Value
            //helloReply.Age = null;
            ////bytes
            //helloReply.SmallPicture = ByteString.CopyFrom(1);
            ////IList<T>
            //helloReply.Roles.Add("123");
            ////IDictionary<TKey, TValue>
            //helloReply.Attributes.Add("key", "value");

            //--- 无结构的条件消息 ---

            //helloReply.Detail=Any.Pack()

            return Task.FromResult(helloReply);
        }
    }
}
