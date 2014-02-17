using EquiMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EquiMarket.DAL
{
    public class EquiContext: DbContext
    {

        public EquiContext()
            : base("EquiContext")
        {
        }

        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}