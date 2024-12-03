using Microsoft.OpenApi.Models;

namespace AuctionWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Enable MVC for views

            // Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Authorization middleware
            app.UseAuthorization();

            // Static files (CSS, JS, etc.)
            app.UseStaticFiles();

            // MVC routing configuration
            app.UseRouting();

            // Configure default controller route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); // Default route to HomeController/Index

            // Run the application
            app.Run();
        }
    }
}
