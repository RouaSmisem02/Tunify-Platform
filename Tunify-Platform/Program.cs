using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;

namespace Tunify_Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appBuilder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string configuration
            string dbConnectionString = appBuilder.Configuration.GetConnectionString("DefaultConnection");

            // Register the DbContext with the dependency injection container
            appBuilder.Services.AddDbContext<TunifyDbContext>(dbOptions => dbOptions.UseSqlServer(dbConnectionString));

            var app = appBuilder.Build();

            app.MapGet("/", () => "Hello, Tunify!");

            app.Run();
        }
    }
}
