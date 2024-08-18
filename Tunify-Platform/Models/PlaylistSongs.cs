using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tunify_Platform.Models
{
    public class PlaylistSongs
    {
        [Key]
        [Column(Order = 0)]
        public int PlaylistId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int SongId { get; set; }

        public Playlists Playlist { get; set; } // Navigation property

        public Songs Song { get; set; } // Navigation property
    }
}
