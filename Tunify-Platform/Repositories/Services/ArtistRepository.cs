using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Data;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

public class ArtistRepository : IArtistRepository
{
    private readonly TunifyDbContext _context;

    public ArtistRepository(TunifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Artists>> GetAllArtistsAsync()
    {
        return await _context.Artists.ToListAsync();
    }

    public async Task<Artists> GetArtistByIdAsync(int id)
    {
        return await _context.Artists.FindAsync(id);
    }

    public async Task AddArtistAsync(Artists artist)
    {
        if (artist == null)
            throw new ArgumentNullException(nameof(artist));

        await _context.Artists.AddAsync(artist);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateArtistAsync(Artists artist)
    {
        if (artist == null)
            throw new ArgumentNullException(nameof(artist));

        var existingArtist = await _context.Artists.FindAsync(artist.Id);
        if (existingArtist == null)
        {
            throw new InvalidOperationException("Artist not found.");
        }

        _context.Entry(existingArtist).CurrentValues.SetValues(artist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteArtistAsync(int id)
    {
        var artist = await _context.Artists.FindAsync(id);
        if (artist != null)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }
    }
}
