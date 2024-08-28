using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

public class SongRepository : ISongRepository
{
    private readonly TunifyDbContext _context;

    public SongRepository(TunifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Songs>> GetAllSongsAsync()
    {
        // Include related entities, not foreign key properties
        return await _context.Songs
                             .Include(s => s.Artist)  // Assuming Artist is a navigation property
                             .Include(s => s.Album)   // Assuming Album is a navigation property
                             .ToListAsync();
    }

    public async Task<Songs> GetSongByIdAsync(int id)
    {
        // Include related entities, not foreign key properties
        return await _context.Songs
                             .Include(s => s.Artist)  // Assuming Artist is a navigation property
                             .Include(s => s.Album)   // Assuming Album is a navigation property
                             .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddSongAsync(Songs song)
    {
        try
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Handle exceptions, log errors if necessary
            throw new Exception("An error occurred while adding the song.", ex);
        }
    }

    public async Task UpdateSongAsync(Songs song)
    {
        try
        {
            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Handle exceptions, log errors if necessary
            throw new Exception("An error occurred while updating the song.", ex);
        }
    }

    public async Task DeleteSongAsync(int id)
    {
        try
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbUpdateException ex)
        {
            // Handle exceptions, log errors if necessary
            throw new Exception("An error occurred while deleting the song.", ex);
        }
    }
}
