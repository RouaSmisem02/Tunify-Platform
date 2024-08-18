﻿using Tunify_Platform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
