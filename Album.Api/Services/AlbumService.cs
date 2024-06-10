using Album.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Services
{
    using Album.Api.Models;
    public class AlbumService : IAlbumService
    {
        private readonly album_DbContext _context;

        public AlbumService(album_DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<Album> GetAlbum(int id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public async Task<bool> PutAlbum(int id, Album album)
        {
            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AlbumExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Album> PostAlbum(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return album;
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return false;
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> AlbumExists(int id)
        {
            return await _context.Albums.AnyAsync(e => e.Id == id);
        }
    }
}
