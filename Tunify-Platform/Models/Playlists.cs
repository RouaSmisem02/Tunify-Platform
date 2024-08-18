namespace Tunify_Platform.Models
{
    public class Playlists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }

        public ICollection<PlaylistSongs> PlaylistSongs { get; set; } // Navigation property
    }
}