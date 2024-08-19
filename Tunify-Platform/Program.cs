using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Repositories;
using Tunify_Platform.Repositories.Interfaces;
using Microsoft.OpenApi.Models;

namespace Tunify_Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string configuration
            string dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register the DbContext with the dependency injection container
            builder.Services.AddDbContext<TunifyDbContext>(options =>
                options.UseSqlServer(dbConnectionString));

            // Register the repository interfaces with their implementations
            builder.Services.AddScoped<IArtists, ArtistRepository>();
            builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            builder.Services.AddScoped<ISongRepository, SongRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Add Swagger services to the container
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tunify API",
                    Version = "v1",
                    Description = "API for managing playlists, songs, and artists in the Tunify Platform"
                });
            });

            var app = builder.Build();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/{documentName}/swagger.json";
            });

            // Enable middleware to serve Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/v1/swagger.json", "Tunify API v1");
                c.RoutePrefix = string.Empty;
            });

            // Define endpoints
            app.MapGet("/", () => "Hello, Tunify!");

            // Add more endpoints or routes if needed
            app.MapControllers(); // Uncomment if you are using controllers

            app.Run();
        }
    }
}
