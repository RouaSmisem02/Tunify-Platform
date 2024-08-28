using Tunify_Platform.Models;

namespace Tunify_Platform.Models
{

    public class Songs
    {
       
            public int Id { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public int Duration { get; set; } // Duration in seconds      
            public int ArtistId { get; set; }
            public virtual Artists Artist { get; set; }

            public int AlbumId { get; set; }
            public virtual Albums Album { get; set; }
      
        public ICollection<PlaylistSongs> PlaylistSongs { get; set; } // Navigation property
    }
}