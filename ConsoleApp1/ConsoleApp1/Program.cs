// See https://aka.ms/new-console-template for more information


using ConsoleApp1;
using Grpc.Core;
using Grpc.Net.Client;

var channel =  GrpcChannel.ForAddress("http://localhost:5285");
var cleint = new Greeter.GreeterClient(channel);
var cancelationToken = new CancellationTokenSource();

DateTime? deadline = DateTime.UtcNow.AddMilliseconds(5000);
object? reply = new object();
try
{
    reply=  await cleint.SayHelloAsync(request:new HelloRequest { Name = "Ahmed" } , deadline:deadline,cancellationToken: cancelationToken.Token );

}
catch (Grpc.Core.RpcException ex)when (ex.StatusCode == StatusCode.DeadlineExceeded)
{
    Console.WriteLine("Server Does not respond with a grpc response.");

}
catch (Exception ex)
{
    Console.WriteLine("Server Ex");
    Console.WriteLine(ex);
}

Console.WriteLine(reply);