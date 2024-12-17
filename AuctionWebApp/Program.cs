using DataAccess;
using DataAccess.Interfaces;
using API.BusinessLogicLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Retrieve and validate the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "Database connection string is missing in appsettings.json.");
}

// Register IAuctionAccess with AuctionDBAccess
builder.Services.AddSingleton<IAuctionAccess>(provider => new AuctionDBAccess(connectionString));

// Register IAuctionItemAccess with AuctionItemDBAccess
builder.Services.AddSingleton<IAuctionItemAccess>(provider => new AuctionItemDBAccess(connectionString));

// Register the business logic layer
builder.Services.AddSingleton<AuctionLogic>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Auction API",
        Version = "v1",
        Description = "API for managing auctions and related resources."
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API V1");
        options.RoutePrefix = "swagger"; // Accessible at /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
