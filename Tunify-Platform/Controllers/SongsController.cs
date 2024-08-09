using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tunify_Platform.Models;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongRepository _songRepository;

        public SongsController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {
            var songs = await _songRepository.GetAllSongsAsync();
            return Ok(songs);
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Songs>> GetSong(int id)
        {
            var song = await _songRepository.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, Songs song)
        {
            if (id != song.Id)
            {
                return BadRequest("Song ID mismatch.");
            }

            try
            {
                await _songRepository.UpdateSongAsync(song);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _songRepository.GetSongByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Songs>> CreateSong(Songs song)
        {
            if (song == null)
            {
                return BadRequest("Song is null.");
            }

            await _songRepository.AddSongAsync(song);
            return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                await _songRepository.DeleteSongAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
