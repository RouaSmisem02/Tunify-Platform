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
            // Define composite primary key for PlaylistSongs
            modelBuilder.Entity<PlaylistSongs>()
                .HasKey(ps => new { ps.PlaylistId, ps.SongId });

            // Define relationships
            modelBuilder.Entity<PlaylistSongs>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId);

            modelBuilder.Entity<PlaylistSongs>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId);

            // Additional configurations (if necessary)
            modelBuilder.Entity<Albums>()
                .HasIndex(a => a.Title)
                .IsUnique();

            modelBuilder.Entity<Artists>()
                .HasIndex(a => a.Name)
                .IsUnique();

            modelBuilder.Entity<Songs>()
                .HasIndex(s => s.Title);

            // Seed data example (optional)
            modelBuilder.Entity<Artists>().HasData(
                new Artists { Id = 1, Name = "Artist1" },
                new Artists { Id = 2, Name = "Artist2" }
            );

            // Other configurations
        }
    }
}
