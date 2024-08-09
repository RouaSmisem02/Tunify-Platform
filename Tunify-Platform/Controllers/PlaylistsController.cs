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
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistsController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlists>>> GetPlaylists()
        {
            var playlists = await _playlistRepository.GetAllPlaylistsAsync();
            return Ok(playlists);
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlists>> GetPlaylist(int id)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        // PUT: api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, Playlists playlist)
        {
            if (id != playlist.Id)
            {
                return BadRequest("Playlist ID mismatch.");
            }

            try
            {
                await _playlistRepository.UpdatePlaylistAsync(playlist);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _playlistRepository.GetPlaylistByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Playlists
        [HttpPost]
        public async Task<ActionResult<Playlists>> CreatePlaylist(Playlists playlist)
        {
            if (playlist == null)
            {
                return BadRequest("Playlist is null.");
            }

            await _playlistRepository.AddPlaylistAsync(playlist);
            return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.Id }, playlist);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            try
            {
                await _playlistRepository.DeletePlaylistAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
