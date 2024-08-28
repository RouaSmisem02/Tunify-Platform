using System.Collections.Generic;
using System.Threading.Tasks;
using Tunify_Platform.Models;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<IEnumerable<Songs>> GetAllSongsAsync();
        Task<Songs> GetSongByIdAsync(int id);
        Task AddSongAsync(Songs song);
        Task UpdateSongAsync(Songs song);
        Task DeleteSongAsync(int id);
    }
}
