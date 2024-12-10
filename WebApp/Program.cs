using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuctionSemesterProject.Services;
using AuctionSemesterProject.DataAccess;
using Microsoft.OpenApi.Models;

namespace AuctionSemesterProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();  // Enable MVC for views

            // Register DAOs and Services for dependency injection
            builder.Services.AddScoped<BidDAO>(sp =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                return new BidDAO(connectionString);
            });

            builder.Services.AddScoped<BidService>(sp =>
            {
                var bidDAO = sp.GetRequiredService<BidDAO>();
                return new BidService(bidDAO);  // Inject BidDAO into BidService
            });

            // Swagger setup for WebApp (MVC)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auction WebApp API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Swagger will now be available at /swagger
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction WebApp API V1");
                    options.RoutePrefix = "swagger";  // Set Swagger UI to be at /swagger
                });
            }

            // Ensure MVC views are still accessible at the root URL
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // MVC routing configuration for website
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the application
            app.Run();
        }
    }
}
