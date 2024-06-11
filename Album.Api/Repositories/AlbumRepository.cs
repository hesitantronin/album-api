using System.Collections.Generic;
using System.Threading.Tasks;
using Album.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Album.Api.Repositories
{
    using Album.Api.Models;
    public class AlbumRepository : IAlbumRepository
    {
        private readonly album_DbContext _context;

        public AlbumRepository(album_DbContext context)
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

        public async Task AddAlbum(Album album)
        {
            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAlbum(Album album)
        {
            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AlbumExists(int id)
        {
            return await _context.Albums.AnyAsync(e => e.Id == id);
        }
    }
}
