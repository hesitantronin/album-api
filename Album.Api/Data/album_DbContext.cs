using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Globalization;
using System.IO;

namespace Album.Api.Data
{
    using Album.Api.Models;
    public class album_DbContext : DbContext
    {
        public album_DbContext(DbContextOptions<album_DbContext> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
    }
}

