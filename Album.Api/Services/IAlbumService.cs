using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Services
{
    using Album.Api.Models;
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAlbums();
        Task<Album> GetAlbum(int id);
        Task<bool> PutAlbum(int id, Album album);
        Task<Album> PostAlbum(Album album);
        Task<bool> DeleteAlbum(int id);
    }
}
