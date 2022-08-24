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
        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<TagGroupEntity> TagGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbFile = Path.Combine(appData, "FileTagger", "tagger.db");

            optionsBuilder.UseSqlite($"Filename={dbFile}");
        }
    }
}
