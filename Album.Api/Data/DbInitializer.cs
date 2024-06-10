using System;
using System.Linq;

namespace Album.Api.Data
{
    using Album.Api.Models;
    public static class DbInitializer
    {
        public static void Initialize(album_DbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any albums.
            if (context.Albums.Any())
            {
                return;   // DB has been seeded
            }

            var albums = new Album[]
            {
                new Album{ Name = "Hesitant Alien", Artist = "Gerard Way", ImageUrl = "https://m.media-amazon.com/images/I/81tSOjPus3L._UF1000,1000_QL80_.jpg"},
                new Album{ Name = "The Black Parade", Artist = "My Chemical Romance", ImageUrl = "https://upload.wikimedia.org/wikipedia/en/e/ea/Blackparadecover.jpg"},
                new Album{ Name = "Kid A", Artist = "Radiohead", ImageUrl = "https://upload.wikimedia.org/wikipedia/en/0/02/Radioheadkida.png"},
                new Album{ Name = "Demon Days", Artist = "Gorillaz", ImageUrl = "https://m.media-amazon.com/images/I/71lix6+VfWL._UF1000,1000_QL80_.jpg"},
                new Album{ Name = "Eternal Blue", Artist = "Spiritbox", ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/94/Spiritbox_EternalBlue.jpg"}
            };

            foreach (Album a in albums)
            {
                context.Albums.Add(a);
            }
            
            context.SaveChanges();

        }
    }
}