using Microsoft.EntityFrameworkCore;
using Mitra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure relationships
            modelBuilder.Entity<EventCategory>()
                .HasMany(e => e.Events)
                .WithOne(vd => vd.Category)
                .HasForeignKey(vd => vd.EventcategoryId);


            //base.OnModelCreating(modelBuilder);
        }
    }
}
