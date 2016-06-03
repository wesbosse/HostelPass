using hostL.API.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace hostL.API.Infrastructure
{
    public class HostLDataContext : IdentityDbContext<HostLUser>
    {
        public HostLDataContext() : base("HostL")
        {
        }

        public static HostLDataContext Create()
        {
            return new HostLDataContext();
        }


        public IDbSet<Amenity> Amenities { get; set; }
        public IDbSet<Hostel> Hostels { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Payment> Payments { get; set; }
        public IDbSet<Rating> Ratings { get; set; }
        public IDbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Configures one to many relationships between HostLUser/Hostel Owner and Hostel 
            modelBuilder.Entity<HostLUser>()
                .HasMany(h => h.Hostels)
                .WithRequired(h => h.HostelOwner)
                .HasForeignKey(h => h.UserId);

            // Configure one to many relationship between HostLUser and Reservation 
            modelBuilder.Entity<HostLUser>()
                 .HasMany(h => h.Reservations)
                 .WithOptional(r => r.Traveller)
                 .HasForeignKey(r => r.UserId);

            // Configure one to many relationship between HostLUser and Rating
            modelBuilder.Entity<HostLUser>()
                 .HasMany(h => h.Ratings)
                 .WithOptional(r => r.Traveller)
                 .HasForeignKey(r => r.TravellerId);

            // Configure one to many relationship between HostLUser and Messages
            modelBuilder.Entity<HostLUser>()
                .HasMany(h => h.Messages)
                .WithOptional(m => m.Traveller)
                .HasForeignKey(m => m.UserId);

            // Configure one to many relationship between Hostel and Rating 
            modelBuilder.Entity<Hostel>()
                .HasMany(h => h.Ratings)
                .WithOptional(r => r.Hostel)
                .HasForeignKey(r => r.HostelId);

            // Configure one to many relationships between Hostel and Reservation 
            modelBuilder.Entity<Hostel>()
                .HasMany(h => h.Reservations)
                .WithRequired(r => r.Hostel)
                .HasForeignKey(r => r.HostelId);

            // Configure one to many relationships between Hostel and Hostel Amenities 
            modelBuilder.Entity<Hostel>()
                .HasMany(h => h.Amenities)
                .WithMany(ha => ha.Hostels)
                .Map(cfg => cfg.ToTable("HostelAmenity"));

            // Configure one to many relationships between Payment and Reservations
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Payments)
                .WithRequired(p => p.Reservation)
                .HasForeignKey(p => p.ReservationId);
                
            base.OnModelCreating(modelBuilder);

        }
    }
}