using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Album.Api;
using Album.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Album.Api.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Album.Api.IntegrationTests
{
    using Microsoft.EntityFrameworkCore;
    using Album.Api.Models;

    public class AlbumApiIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        // Bingbong

        private readonly WebApplicationFactory<Startup> _factory;

        public AlbumApiIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services
                        .SingleOrDefault(d => d.ServiceType ==
                                              typeof(DbContextOptions<album_DbContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<album_DbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<album_DbContext>();

                        db.Database.EnsureCreated();

                        SeedTestData(db);
                    }
                });
            });
        }

        private void SeedTestData(album_DbContext context)
        {
            context.Albums.RemoveRange(context.Albums);
            context.SaveChanges();

            context.Albums.AddRange(
                new Album { Id = 1, Name = "The Black Parade", Artist = "My Chemical Romance", ImageUrl = "https://example.com/album1.jpg" },
                new Album { Id = 2, Name = "Absolution", Artist = "Muse", ImageUrl = "https://example.com/album2.jpg" },
                new Album { Id = 3, Name = "OK Computer", Artist = "Radiohead", ImageUrl = "https://example.com/album3.jpg" }
            );
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAlbums_ReturnsAllAlbums()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/album");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var albums = JsonConvert.DeserializeObject<Album[]>(responseString);

            // Assert
            Assert.Equal(3, albums.Length);
            Assert.Contains(albums, album => album.Name == "The Black Parade" && album.Artist == "My Chemical Romance" && album.ImageUrl == "https://example.com/album1.jpg");
            Assert.Contains(albums, album => album.Name == "Absolution" && album.Artist == "Muse" && album.ImageUrl == "https://example.com/album2.jpg");
            Assert.Contains(albums, album => album.Name == "OK Computer" && album.Artist == "Radiohead" && album.ImageUrl == "https://example.com/album3.jpg");
        }

        [Fact]
        public async Task GetAlbum_ReturnsAlbumById()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/album/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var album = JsonConvert.DeserializeObject<Album>(responseString);

            // Assert
            Assert.Equal(1, album.Id);
            Assert.Equal("The Black Parade", album.Name);
            Assert.Equal("My Chemical Romance", album.Artist);
            Assert.Equal("https://example.com/album1.jpg", album.ImageUrl);
        }

        [Fact]
        public async Task PostAlbum_CreatesNewAlbum()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newAlbum = new Album { Name = "Origin of Symmetry", Artist = "Muse", ImageUrl = "https://example.com/album4.jpg" };
            var content = new StringContent(JsonConvert.SerializeObject(newAlbum), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/album", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var createdAlbum = JsonConvert.DeserializeObject<Album>(responseString);

            // Assert
            Assert.NotNull(createdAlbum);
            Assert.Equal("Origin of Symmetry", createdAlbum.Name);
            Assert.Equal("Muse", createdAlbum.Artist);
            Assert.Equal("https://example.com/album4.jpg", createdAlbum.ImageUrl);
        }

        [Fact]
        public async Task DeleteAlbum_RemovesAlbumById()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/album/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("NoContent", response.StatusCode.ToString());
        }

    }
}