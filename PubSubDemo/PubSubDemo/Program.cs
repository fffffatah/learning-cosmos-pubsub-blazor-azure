//using BusinessLogicLayer;
using Microsoft.Azure.WebPubSub.Common;
using Azure.Messaging.WebPubSub;
using Microsoft.Azure.WebPubSub.AspNetCore;
using PubSubDemo.Controllers;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<MessageLogic>();
builder.Services.AddWebPubSub(
    o => o.ServiceEndpoint = new ServiceEndpoint(Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT_BS")))
    .AddWebPubSubServiceClient<PubSubEventHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.UseRouting();
//map pubsub event handler
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapWebPubSubHub<PubSubEventHandler>("/eventhandler/{*path}");
});

app.Run();
