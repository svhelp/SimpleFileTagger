using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class TaggerContext : DbContext
    {
        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<TagGroupEntity> TagGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=tagger.db");
        }
    }
}
