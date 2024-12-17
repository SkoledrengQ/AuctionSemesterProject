using WebApp.BusinessLogicLayer;
using WebApp.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register AuctionLogicWeb as Scoped
builder.Services.AddScoped<AuctionLogicWeb>();
builder.Services.AddScoped<BidLogic>();

// Register IAuctionService with HttpClient
builder.Services.AddHttpClient<IAuctionService, AuctionService>((serviceProvider, httpClient) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var baseUrl = configuration["ApiSettings:BaseUrl"];

    if (string.IsNullOrEmpty(baseUrl))
        throw new ArgumentNullException(nameof(baseUrl), "API base URL is not configured in appsettings.json.");

    httpClient.BaseAddress = new Uri(baseUrl);
});

// Register IBidService with BidService
builder.Services.AddScoped<IBidService, BidService>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
