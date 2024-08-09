namespace Tunify_Platform.Models
{
    public class Playlists
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public ICollection<PlaylistSongs> Songs { get; set; }
    }
}