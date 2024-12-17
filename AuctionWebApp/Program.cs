using DataAccess;
using DataAccess.Interfaces;
using API.BusinessLogicLayer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "Database connection string is missing in appsettings.json.");
}


builder.Services.AddSingleton<IAuctionAccess>(provider => new AuctionDBAccess(connectionString));
builder.Services.AddSingleton<IBidAccess>(provider => new BidDBAccess(connectionString));
builder.Services.AddSingleton<IAuctionItemAccess>(provider => new AuctionItemDBAccess(connectionString));


builder.Services.AddSingleton<AuctionLogic>();
builder.Services.AddSingleton<BidLogic>();
builder.Services.AddSingleton<AuctionItemLogic>();



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


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API V1");
        options.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
