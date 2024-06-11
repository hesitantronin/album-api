using System.Collections.Generic;
using System.Threading.Tasks;
using Album.Api.Repositories;

namespace Album.Api.Services
{
    using Album.Api.Models;
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _albumRepository.GetAlbums();
        }

        public async Task<Album> GetAlbum(int id)
        {
            return await _albumRepository.GetAlbum(id);
        }

        public async Task<bool> PutAlbum(int id, Album album)
        {
            if (!await _albumRepository.AlbumExists(id))
            {
                return false;
            }

            await _albumRepository.UpdateAlbum(album);
            return true;
        }

        public async Task<Album> PostAlbum(Album album)
        {
            await _albumRepository.AddAlbum(album);
            return album;
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            if (!await _albumRepository.AlbumExists(id))
            {
                return false;
            }

            await _albumRepository.DeleteAlbum(id);
            return true;
        }
    }
}
