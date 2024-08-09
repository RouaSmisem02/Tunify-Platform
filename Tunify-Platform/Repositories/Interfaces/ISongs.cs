using Tunify_Platform.Models;

public interface ISongRepository
{
    Task<IEnumerable<Songs>> GetAllSongsAsync();
    Task<Songs> GetSongByIdAsync(int id);
    Task AddSongAsync(Songs song);
    Task UpdateSongAsync(Songs song);
    Task DeleteSongAsync(int id);
}
