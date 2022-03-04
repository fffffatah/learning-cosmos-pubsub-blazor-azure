
using CosmosRepositoryPatternCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCosmosRepository(
        options =>
        {
            /// <summary>
            /// Container name and partition key path can be defined here as well
            /// in container options for an Entity.
            /// </summary>
            //options.CosmosConnectionString = Environment.GetEnvironmentVariable("COSMOS_STRING");
            options.CosmosConnectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
            //options.ContainerId = "data-store";
            options.DatabaseId = "fffffatah";
            options.ContainerPerItemType = true;
            options.OptimizeBandwidth = true;
            //options.ContainerBuilder.Configure<Book>(userContainerOptions =>
            //{
            //    userContainerOptions.WithContainer("books");
            //    userContainerOptions.WithPartitionKey("/genre");
            //    userContainerOptions.WithChangeFeedMonitoring();
            //});
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
