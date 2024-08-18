using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Repositories;
using Tunify_Platform.Repositories.Interfaces;

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

            // Register the repository interfaces with their implementations
            appBuilder.Services.AddScoped<IArtists, ArtistRepository>();
            appBuilder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            appBuilder.Services.AddScoped<ISongRepository, SongRepository>();
            appBuilder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = appBuilder.Build();

            // Define endpoints
            app.MapGet("/", () => "Hello, Tunify!");

            // Add more endpoints or routes if needed
            // app.MapControllers(); // Uncomment if you are using controllers

            app.Run();
        }
    }
}
