using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Repositories
{
    using Album.Api.Models;
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAlbums();
        Task<Album> GetAlbum(int id);
        Task AddAlbum(Album album);
        Task UpdateAlbum(Album album);
        Task DeleteAlbum(int id);
        Task<bool> AlbumExists(int id);
    }
}
