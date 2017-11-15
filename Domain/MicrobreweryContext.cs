using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Domain
{
    public class MicrobreweryContext : DbContext
    {
        public MicrobreweryContext(DbContextOptions<MicrobreweryContext> options)
            : base(options)
        {
        }

        public DbSet<Microbrewery> Microbreweries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Microbrewery>().ToTable("Microbrewery");
            modelBuilder.Entity<Microbrewery>().HasKey("Id");

            modelBuilder.Entity<Beer>().ToTable("Beer");
            modelBuilder.Entity<Beer>().HasKey("Id");
        }
    }
}
