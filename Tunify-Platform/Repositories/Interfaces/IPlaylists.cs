using Tunify_Platform.Models;

public interface IPlaylistRepository
{
    Task<IEnumerable<Playlists>> GetAllPlaylistsAsync();
    Task<Playlists> GetPlaylistByIdAsync(int id);
    Task AddPlaylistAsync(Playlists playlist);
    Task UpdatePlaylistAsync(Playlists playlist);
    Task DeletePlaylistAsync(int id);
}
