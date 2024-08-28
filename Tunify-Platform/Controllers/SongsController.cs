using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly TunifyDbContext _context;

        public SongRepository(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Songs>> GetAllSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<Songs> GetSongByIdAsync(int id)
        {
            return await _context.Songs.FindAsync(id);
        }

        public async Task AddSongAsync(Songs song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSongAsync(Songs song)
        {
            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSongAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                throw new KeyNotFoundException($"Song with id {id} not found.");
            }
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }
    }
}
