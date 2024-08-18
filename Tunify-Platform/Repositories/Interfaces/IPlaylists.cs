using Tunify_Platform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Playlists>> GetAllPlaylistsAsync();
        Task<Playlists> GetPlaylistByIdAsync(int id);
        Task AddPlaylistAsync(Playlists playlist);
        Task UpdatePlaylistAsync(Playlists playlist);
        Task DeletePlaylistAsync(int id);
    }
}
