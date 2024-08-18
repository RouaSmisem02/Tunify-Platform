// File: IArtistRepository.cs
using Tunify_Platform.Models;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IArtists
    {
        Task<IEnumerable<Artists>> GetAllArtistsAsync();
        Task<Artists> GetArtistByIdAsync(int id);
        Task AddArtistAsync(Artists artist);
        Task UpdateArtistAsync(Artists artist);
        Task DeleteArtistAsync(int id);
    }
}
