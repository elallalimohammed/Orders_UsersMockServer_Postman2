using MongoDB.Driver;
using Orders_UsersMockServer_Postman.Repositories;
using Orders_UsersMockServer_Postman.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<IOrderService, OrderService>();


// 1️⃣ Create a single MongoClient instance
var mongoClient = new MongoClient("mongodb://localhost:27017");

// 2️⃣ Get the specific database
var mongoDatabase = mongoClient.GetDatabase("OrdersDB_MockUsers");

// 3️⃣ Register `IMongoDatabase` for dependency injection
builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);

// 4️⃣ Register the repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();



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
