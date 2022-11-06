using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaggerContext : DbContext
    {
        public TaggerContext()
        {

        }

        public TaggerContext(DbContextOptions<TaggerContext> options)
            : base(options)
        {

        }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<TagGroupEntity> TagGroups { get; set; }

        public DbSet<ThumbnailEntity> Thumbnails { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbFile = Path.Combine(appData, "FileTagger", "tagger.db");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite($"Filename={dbFile}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagEntity>()
                .HasOne(p => p.Group)
                .WithMany(b => b.Tags)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
