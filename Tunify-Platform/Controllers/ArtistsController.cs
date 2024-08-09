using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistsController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artists>>> GetArtists()
        {
            var artists = await _artistRepository.GetAllArtistsAsync();
            return Ok(artists);
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artists>> GetArtist(int id)
        {
            var artist = await _artistRepository.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, Artists artist)
        {
            if (id != artist.Id)
            {
                return BadRequest("Artist ID mismatch.");
            }

            try
            {
                await _artistRepository.UpdateArtistAsync(artist);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<Artists>> PostArtist(Artists artist)
        {
            if (artist == null)
            {
                return BadRequest("Artist is null.");
            }

            await _artistRepository.AddArtistAsync(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            try
            {
                await _artistRepository.DeleteArtistAsync(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
