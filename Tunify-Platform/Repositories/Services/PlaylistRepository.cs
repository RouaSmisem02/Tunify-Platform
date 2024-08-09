using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly TunifyDbContext _context;

    public PlaylistRepository(TunifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Playlists>> GetAllPlaylistsAsync()
    {
        return await _context.Playlists.Include(p => p.UserId).ToListAsync();
    }

    public async Task<Playlists> GetPlaylistByIdAsync(int id)
    {
        return await _context.Playlists.Include(p => p.UserId).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddPlaylistAsync(Playlists playlist)
    {
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePlaylistAsync(Playlists playlist)
    {
        _context.Playlists.Update(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePlaylistAsync(int id)
    {
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist != null)
        {
            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
        }
    }
}
