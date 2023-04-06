using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class FmDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=FlightManagerDatabase;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FlightManagerDatabase;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserModelCreating(modelBuilder);
            UserRoleModelCreating(modelBuilder);
            UserTicketModelCreating(modelBuilder);
            
            ReservationModelCreating(modelBuilder);
            ReservationTicketModelCreating(modelBuilder);
            
            TicketModelCreating(modelBuilder);
            TicketTypeModelCreating(modelBuilder);
            FlightModelCreating(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
        private void UserModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.EGN)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Mobile)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        private void UserRoleModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.ID);

            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => ur.Name)
                .IsUnique();
        }

        private void UserTicketModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTicket>()
                 .HasKey(ut => new { ut.UserID, ut.TicketID });

            modelBuilder.Entity<UserTicket>()
                .HasOne(ut => ut.User)
                .WithMany(ut => ut.Tickets)
                .IsRequired();

            modelBuilder.Entity<UserTicket>()
                .HasOne(ur => ur.Ticket)
                .WithOne(ur => ur.UserTicket)
                .IsRequired();

            modelBuilder.Entity<UserTicket>()
                .HasIndex(ut => ut.TicketID)
                .IsUnique();
        }

        private void ReservationModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.ID);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.EGN)
                .IsUnique();

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.Mobile)
                .IsUnique();
        }

        private void ReservationTicketModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationTicket>()
                 .HasKey(ut => new { ut.ReservationID, ut.TicketID });

            modelBuilder.Entity<ReservationTicket>()
                .HasOne(ut => ut.Reservation)
                .WithMany(ut => ut.Tickets)
                .IsRequired();

            modelBuilder.Entity<ReservationTicket>()
                .HasOne(ur => ur.Ticket)
                .WithOne(ur => ur.ReservationTicket)
                .IsRequired();

            modelBuilder.Entity<ReservationTicket>()
                .HasIndex(ut => ut.TicketID)
                .IsUnique();
        }

        private void TicketModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasKey(t => t.ID);

            modelBuilder.Entity<Ticket>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(t => t.Tickets)
                .IsRequired();
        }

        private void TicketTypeModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketType>()
                .HasKey(tt => tt.ID);

            modelBuilder.Entity<TicketType>()
                .HasIndex(tt => tt.Name)
                .IsUnique();
        }

        private void FlightModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasKey(f => f.ID);

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.PlaneID)
                .IsUnique();
        }



        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UsersRoles { get; set; }
        public virtual DbSet<UserTicket> UsersTickets { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationTicket> ReservationsTickets { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketType> TicketsTypes { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
    }
}
