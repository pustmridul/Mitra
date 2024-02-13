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
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Expectation> Expectations { get; set; }
        public DbSet<Street> Streets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure relationships
            modelBuilder.Entity<EventCategory>()
                .HasMany(e => e.Events)
                .WithOne(vd => vd.EventCategory)
                .HasForeignKey(vd => vd.EventcategoryId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Donors)
                .WithOne(vd => vd.User)
                .HasForeignKey(vd => vd.userId);

            modelBuilder.Entity<Street>()
                .HasMany(e => e.Donors)
                .WithOne(vd => vd.Street)
                .HasForeignKey(vd => vd.StreetId);



            modelBuilder.Entity<Donation>(entity =>
            {
                // Configure primary key
                entity.HasKey(d => d.Id);

                // Configure Amount property
                entity.Property(d => d.Amount)
                    .HasColumnType("decimal(18,2)"); // Adjust the precision and scale as needed

                // Configure foreign key relationships
                entity.HasOne(d => d.Event)
                    .WithMany(e => e.Donations)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Restrict); // or Cascade, SetNull, etc. based on your requirements

                entity.HasOne(d => d.Donor)
                    .WithMany(donor => donor.Donations)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.Restrict); // or Cascade, SetNull, etc. based on your requirements
            });

            modelBuilder.Entity<Expectation>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Amount)
                       .HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Event)
                    .WithMany(e => e.Expectations)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Restrict); // or Cascade, SetNull, etc. based on your requirements

                entity.HasOne(d => d.Donor)
                    .WithMany(donor => donor.Expectations)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            


            base.OnModelCreating(modelBuilder);
        }
    }
}
