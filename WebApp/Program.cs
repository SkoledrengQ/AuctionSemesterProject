using AuctionSemesterProject.Services;
using AuctionSemesterProject.DataAccess;
using Microsoft.OpenApi.Models; // Ensure this namespace is included

namespace AuctionSemesterProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services for the WebApp project
            builder.Services.AddControllersWithViews();  // Add MVC controllers

            // Register shared DAOs and Services for dependency injection
            builder.Services.AddSingleton<AuctionDAO>(sp =>
                new AuctionDAO(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddTransient<AuctionService>();

            builder.Services.AddSingleton<AddressDAO>(sp =>
                new AddressDAO(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddTransient<AddressService>();

            builder.Services.AddSingleton<AuctionItemDAO>(sp =>
                new AuctionItemDAO(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddTransient<AuctionItemService>();

            builder.Services.AddSingleton<BidDAO>(sp =>
                new BidDAO(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddTransient<BidService>();

            builder.Services.AddSingleton<EmployeeDAO>(sp =>
                new EmployeeDAO(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddTransient<EmployeeService>();

            builder.Services.AddSingleton<MemberDAO>(sp =>
                new MemberDAO(builder.Configuration.GetConnectionString("DefaultConnection")!));
            builder.Services.AddTransient<MemberService>();

            // Register Swagger services
            builder.Services.AddEndpointsApiExplorer(); // Required for Swagger in minimal APIs
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auction WebApp API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline for the WebApp project
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction WebApp API V1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // MVC routing configuration for the WebApp
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
