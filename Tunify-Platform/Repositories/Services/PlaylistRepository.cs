using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly TunifyDbContext _context;

    public PlaylistRepository(TunifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Playlists>> GetAllPlaylistsAsync()
    {
        return await _context.Playlists.Include(p => p.User).ToListAsync();
    }

    public async Task<Playlists> GetPlaylistByIdAsync(int id)
    {
        var playlist = await _context.Playlists
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (playlist == null)
        {
            // Optionally handle the case where the playlist is not found.
            // e.g., throw new KeyNotFoundException("Playlist not found.");
        }

        return playlist;
    }

    public async Task AddPlaylistAsync(Playlists playlist)
    {
        if (playlist == null)
        {
            throw new ArgumentNullException(nameof(playlist));
        }

        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePlaylistAsync(Playlists playlist)
    {
        if (playlist == null)
        {
            throw new ArgumentNullException(nameof(playlist));
        }

        var existingPlaylist = await _context.Playlists.FindAsync(playlist.Id);
        if (existingPlaylist == null)
        {
            throw new KeyNotFoundException("Playlist not found.");
        }

        _context.Entry(existingPlaylist).CurrentValues.SetValues(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePlaylistAsync(int id)
    {
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist == null)
        {
            throw new KeyNotFoundException("Playlist not found.");
        }

        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();
    }
}
