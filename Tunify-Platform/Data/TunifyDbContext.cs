using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Models;

namespace Tunify_Platform.Data
{
    public class TunifyDbContext : DbContext
    {
        public TunifyDbContext(DbContextOptions<TunifyDbContext> options) : base(options)
        {
        }

        public DbSet<Albums> Albums { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Playlists> Playlists { get; set; }
        public DbSet<PlaylistSongs> PlaylistSongs { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, Name = "Raghad", EmailAddress = "raghad@gmail.com", DateJoined = "2024", SubscriptionId = 1 }
            );
            modelBuilder.Entity<Songs>().HasData(
                new Songs { Id = 1, AlbumId = 1, ArtistId = 1, LengthInSeconds = 30, Name = "First Song", GenreId = 1 }
            );
            modelBuilder.Entity<Playlists>().HasData(
                new Playlists { Id = 1, DateCreated = "2024", Name = "First Playlist", UserId = 1 }
            );
        }
    }
}
