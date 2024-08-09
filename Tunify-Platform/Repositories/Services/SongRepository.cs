using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SongRepository : ISongRepository
{
    private readonly TunifyDbContext _context;

    public SongRepository(TunifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Songs>> GetAllSongsAsync()
    {
        return await _context.Songs.Include(s => s.ArtistId).Include(s => s.AlbumId).ToListAsync();
    }

    public async Task<Songs> GetSongByIdAsync(int id)
    {
        return await _context.Songs.Include(s => s.ArtistId).Include(s => s.AlbumId).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddSongAsync(Songs song)
    {
        await _context.Songs.AddAsync(song);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(Songs song)
    {
        _context.Songs.Update(song);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSongAsync(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song != null)
        {
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }
    }
}
