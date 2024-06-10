using Album.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Controllers
{
    using Album.Api.Models;

    /// <summary>
    /// API Controller for managing albums.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        /// <summary>
        /// Gets all albums.
        /// </summary>
        /// <returns>A list of albums.</returns>
        /// <response code="200">Returns the list of albums</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            var albums = await _albumService.GetAlbums();
            return Ok(albums);
        }

        /// <summary>
        /// Retrieves a specific album by unique id.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>The album with the specified id.</returns>
        /// <response code="200">Album retrieved</response>
        /// <response code="404">Album was not found</response>
        /// <response code="500">Cannot find albums right now</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _albumService.GetAlbum(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        /// <summary>
        /// Updates an album.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="album">The album to update.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        /// <response code="204">Album updated successfully</response>
        /// <response code="400">The album id does not match the URL id</response>
        /// <response code="404">Album was not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
            if (id != album.Id)
            {
                return BadRequest();
            }

            var result = await _albumService.PutAlbum(id, album);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="album">The album to create.</param>
        /// <returns>The created album.</returns>
        /// <response code="201">Album created successfully</response>
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
            var createdAlbum = await _albumService.PostAlbum(album);
            return CreatedAtAction(nameof(GetAlbum), new { id = createdAlbum.Id }, createdAlbum);
        }

        /// <summary>
        /// Deletes a specific album by unique id.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        /// <response code="204">Album deleted successfully</response>
        /// <response code="404">Album was not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await _albumService.DeleteAlbum(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
