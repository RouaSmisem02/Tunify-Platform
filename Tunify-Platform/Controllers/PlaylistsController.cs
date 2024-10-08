﻿using System.Collections.Generic;
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
        private readonly ISongRepository _songRepository;

        public PlaylistsController(IPlaylistRepository playlistRepository, ISongRepository songRepository)
        {
            _playlistRepository = playlistRepository;
            _songRepository = songRepository;
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

        [HttpPost("playlists/{playlistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(playlistId);
            var song = await _songRepository.GetSongByIdAsync(songId);

            if (playlist == null || song == null)
            {
                return NotFound();
            }

            var playlistSong = new PlaylistSongs
            {
                PlaylistId = playlistId,
                SongId = songId,
                Playlist = playlist,
                Song = song
            };

            if (playlist.PlaylistSongs == null)
            {
                playlist.PlaylistSongs = new List<PlaylistSongs>();
            }

            playlist.PlaylistSongs.Add(playlistSong);
            await _playlistRepository.UpdatePlaylistAsync(playlist);

            return NoContent();
        }

    }
}
