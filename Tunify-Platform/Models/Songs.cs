using Tunify_Platform.Models;

namespace Tunify_Platform.Models
{

    public class Songs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        public int LengthInSeconds { get; set; }
        public int GenreId { get; set; }

        public ICollection<PlaylistSongs> PlaylistSongs { get; set; } // Navigation property
    }
}