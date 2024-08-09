using Tunify_Platform.Models;

public interface IArtistRepository
{
    Task<IEnumerable<Artists>> GetAllArtistsAsync();
    Task<Artists> GetArtistByIdAsync(int id);
    Task AddArtistAsync(Artists artist);
    Task UpdateArtistAsync(Artists artist);
    Task DeleteArtistAsync(int id);
}
