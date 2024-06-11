using Album.Api.Controllers;
using Album.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Album.Api.Tests
{
    using Album.Api.Models;
    public class AlbumControllerTests
    {
        private readonly Mock<IAlbumService> _albumServiceMock;
        private readonly AlbumController _controller;

        public AlbumControllerTests()
        {
            _albumServiceMock = new Mock<IAlbumService>();
            _controller = new AlbumController(_albumServiceMock.Object);
        }

        [Fact]
        public async Task GetAlbums_ReturnsOkResult_WithListOfAlbums()
        {
            // Arrange
            var albums = new List<Album> { new Album { Id = 1, Name = "Test Album" } };
            _albumServiceMock.Setup(s => s.GetAlbums()).ReturnsAsync(albums);

            // Act
            var result = await _controller.GetAlbums();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAlbums = Assert.IsType<List<Album>>(okResult.Value);
            Assert.Single(returnAlbums);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)] 
        [InlineData(0)] 
        [InlineData(999999)] 
        public async Task GetAlbum_ReturnsOkResult_WithAlbum(int albumId)
        {
            // Arrange
            var album = new Album { Id = albumId, Name = "Test Album" };
            _albumServiceMock.Setup(s => s.GetAlbum(albumId)).ReturnsAsync(album);

            // Act
            var result = await _controller.GetAlbum(albumId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAlbum = Assert.IsType<Album>(okResult.Value);
            Assert.Equal(albumId, returnAlbum.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(0)] 
        [InlineData(999999)] 
        public async Task GetAlbum_ReturnsNotFound_WhenAlbumDoesNotExist(int albumId)
        {
            // Arrange
            _albumServiceMock.Setup(s => s.GetAlbum(albumId)).ReturnsAsync((Album)null);

            // Act
            var result = await _controller.GetAlbum(albumId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(-1, -1)]
        public async Task PutAlbum_ReturnsNoContent_WhenSuccessful(int albumId, int albumModelId)
        {
            // Arrange
            var album = new Album { Id = albumModelId, Name = "Updated Album" };
            _albumServiceMock.Setup(s => s.PutAlbum(albumId, album)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutAlbum(albumId, album);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public async Task PutAlbum_ReturnsBadRequest_WhenIdDoesNotMatch(int albumId, int albumModelId)
        {
            // Arrange
            var album = new Album { Id = albumModelId, Name = "Updated Album" };

            // Act
            var result = await _controller.PutAlbum(albumId, album);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)] 
        [InlineData(0)]
        public async Task PutAlbum_ReturnsNotFound_WhenAlbumDoesNotExist(int albumId)
        {
            // Arrange
            var album = new Album { Id = albumId, Name = "Updated Album" };
            _albumServiceMock.Setup(s => s.PutAlbum(albumId, album)).ReturnsAsync(false);

            // Act
            var result = await _controller.PutAlbum(albumId, album);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAlbum_ReturnsCreatedAtAction_WithCreatedAlbum()
        {
            // Arrange
            var album = new Album { Id = 1, Name = "New Album" };
            _albumServiceMock.Setup(s => s.PostAlbum(album)).ReturnsAsync(album);

            // Act
            var result = await _controller.PostAlbum(album);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnAlbum = Assert.IsType<Album>(createdAtActionResult.Value);
            Assert.Equal(album.Id, returnAlbum.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)] 
        [InlineData(0)]
        public async Task DeleteAlbum_ReturnsNoContent_WhenSuccessful(int albumId)
        {
            // Arrange
            _albumServiceMock.Setup(s => s.DeleteAlbum(albumId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAlbum(albumId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)] 
        [InlineData(0)]
        public async Task DeleteAlbum_ReturnsNotFound_WhenAlbumDoesNotExist(int albumId)
        {
            // Arrange
            _albumServiceMock.Setup(s => s.DeleteAlbum(albumId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAlbum(albumId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
