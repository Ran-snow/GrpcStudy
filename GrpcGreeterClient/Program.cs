using System;
using System.Threading.Tasks;

using Grpc.Net.Client;

using Greet.V1;
using AutoMapper;
using Greeter.Infrastructure.DTO;
using System.Collections.Generic;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTime, Google.Protobuf.WellKnownTypes.Timestamp>().ConvertUsing(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src));
                cfg.CreateMap<Google.Protobuf.WellKnownTypes.Timestamp, DateTime>().ConvertUsing(src => src.ToDateTime());

                cfg.CreateMap<TimeSpan, Google.Protobuf.WellKnownTypes.Duration>().ConvertUsing(src => Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(src));
                cfg.CreateMap<Google.Protobuf.WellKnownTypes.Duration, TimeSpan>().ConvertUsing(src => src.ToTimeSpan());

                cfg.CreateMap<HelloReply, HelloReplyDTO>();
            });

            var mapper = config.CreateMapper();

            //HelloReplyDTO helloRequestDTO = new HelloReplyDTO
            //{
            //    Age = null,
            //    Attributes = new Dictionary<string, string> { { "1", "2" } },
            //    Message = "123",
            //    Roles = new List<string> { "123", "456" },
            //    SmallPicture = new byte[] { 1, 2, 3 },
            //    Start = DateTime.Now
            //};

            //var helloGRPC = mapper.Map<HelloReplyDTO, HelloRequest>(helloRequestDTO);

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greet.V1.Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });

            var reds = mapper.Map<HelloReplyDTO>(reply);

            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
