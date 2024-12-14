using AuctionSemesterProject.Services;
using AuctionSemesterProject.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AuctionSemesterProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container (API controllers only)
            builder.Services.AddControllers();

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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
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
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();

            // API routing configuration for controllers
            app.MapControllers();  // Maps API controllers to endpoints

            // Run the application
            app.Run();
        }
    }
}
