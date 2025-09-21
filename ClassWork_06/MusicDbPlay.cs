using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Сlass04
{

    
    internal class MusicDbAppDbContext : DbContext
    {
        private readonly string connectionString;

        public MusicDbAppDbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MusicDBConnection"]?.ConnectionString
                               ?? throw new InvalidOperationException("Connection string 'MusicDBConnection' not found.");

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
