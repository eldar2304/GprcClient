using Grpc.Net.Client;
using Google.Protobuf;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using GrpcUserAccountClient;


var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new  UserAccount.UserAccountClient(channel);


app.MapGet("/api/clients/{id}", (Int32 id) =>
    {
        UserReply userReply = client.GetUserById(
                  new UserIdRequest { Id = id });
        return Results.Json(userReply);
    });


app.MapGet("/api/accounts/{id}", (Int32 id) =>
{
    UserReply userReply = client.GetUserById(
              new UserIdRequest { Id = id });
    return Results.Json(userReply);
});


/*
var reply = await client.GetUserByIdAsync(
                  new UserIdRequest { Id = 1 });
Console.WriteLine("User: " + reply.Name + "   Phone:" + reply.Phone.ToString());

var replyAcc = await client.GetAccountByUserIdAsync(
                  new UserIdRequest { Id = 1 });
Console.WriteLine("Num: " + replyAcc.AccountNum + "   Type:" + replyAcc.AccoutType);
Console.WriteLine("Press any key to exit...");
*/

app.Run();