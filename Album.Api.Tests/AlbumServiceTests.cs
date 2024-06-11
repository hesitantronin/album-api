using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Api.Services;
using Moq;
using Album.Api.Repositories;
using Xunit;

namespace Album.Api.Tests
{
    using Album.Api.Models;

    public class AlbumServiceTests
    {
        private readonly Mock<IAlbumRepository> _albumRepositoryMock;
        private readonly AlbumService _albumService;

        public AlbumServiceTests()
        {
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumService = new AlbumService(_albumRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAlbums_ReturnsAllAlbums()
        {
            // Arrange
            var albums = new List<Album>
            {
                new Album { Id = 1, Name = "Test Album 1" },
                new Album { Id = 2, Name = "Test Album 2" }
            };
            _albumRepositoryMock.Setup(repo => repo.GetAlbums()).ReturnsAsync(albums);

            // Act
            var result = await _albumService.GetAlbums();

            // Assert
            Assert.Equal(albums.Count, result.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAlbum_ReturnsAlbum_WhenAlbumExists(int albumId)
        {
            // Arrange
            var album = new Album { Id = albumId, Name = $"Test Album {albumId}" };
            _albumRepositoryMock.Setup(repo => repo.GetAlbum(albumId)).ReturnsAsync(album);

            // Act
            var result = await _albumService.GetAlbum(albumId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(albumId, result.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task PutAlbum_ReturnsTrue_WhenUpdateIsSuccessful(int albumId)
        {
            // Arrange
            var album = new Album { Id = albumId, Name = $"Test Album {albumId}" };
            _albumRepositoryMock.Setup(repo => repo.AlbumExists(albumId)).ReturnsAsync(true);

            // Act
            var result = await _albumService.PutAlbum(albumId, album);

            // Assert
            Assert.True(result);
            _albumRepositoryMock.Verify(repo => repo.UpdateAlbum(album), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task PostAlbum_ReturnsAlbum_WhenAddingIsSuccessful(int albumId)
        {
            // Arrange
            var album = new Album { Id = albumId, Name = $"Test Album {albumId}" };

            // Act
            var result = await _albumService.PostAlbum(album);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(albumId, result.Id);
            _albumRepositoryMock.Verify(repo => repo.AddAlbum(album), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteAlbum_ReturnsTrue_WhenDeleteIsSuccessful(int albumId)
        {
            // Arrange
            _albumRepositoryMock.Setup(repo => repo.AlbumExists(albumId)).ReturnsAsync(true);

            // Act
            var result = await _albumService.DeleteAlbum(albumId);

            // Assert
            Assert.True(result);
            _albumRepositoryMock.Verify(repo => repo.DeleteAlbum(albumId), Times.Once);
        }
    }
}
