using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
                .Property(p => p.ID).ValueGeneratedNever();

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
                .Property(p => p.ID).ValueGeneratedNever();

            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => ur.Name)
                .IsUnique();
        }

        private void UserTicketModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTicket>()
                 .HasKey(ut => new { ut.UserID, ut.TicketID });
            modelBuilder.Entity<UserTicket>()
                .Property(p => p.UserID).ValueGeneratedNever();
            modelBuilder.Entity<UserTicket>()
                .Property(p => p.TicketID).ValueGeneratedNever();

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
            .Property(p => p.ID).ValueGeneratedNever();

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
                .Property(p => p.ReservationID).ValueGeneratedNever();
            modelBuilder.Entity<ReservationTicket>()
                .Property(p => p.TicketID).ValueGeneratedNever();

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
                .Property(p => p.ID).ValueGeneratedNever();

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
                .Property(p => p.ID).ValueGeneratedNever();

            modelBuilder.Entity<TicketType>()
                .HasIndex(tt => tt.Name)
                .IsUnique();
        }

        private void FlightModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasKey(f => f.ID);
            modelBuilder.Entity<Flight>()
                .Property(p => p.ID).ValueGeneratedNever();

            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.PlaneID)
                .IsUnique();
        }

        public override int SaveChanges()
        {
            ClearTablesConnection();
            ConnectTables();
            return base.SaveChanges();
        }

        public void ConnectTables()
        {
            foreach (User user in this.Users)
            {
                UserRole userRole = this.UsersRoles.Where(ur => ur.ID == user.RoleID).First();
                userRole.Users.Add(user);
                user.Role = userRole;

                UserTicket[] userTickets = this.UsersTickets.Where(ut => ut.UserID == user.ID).ToArray();
                foreach (UserTicket userTicket in userTickets)
                {
                    user.Tickets.Add(userTicket);
                    userTicket.User = user;
                }
            }

            foreach (Reservation reservation in this.Reservations)
            {
                ReservationTicket[] reservationTickets = this.ReservationsTickets.Where(rt => rt.ReservationID == reservation.ID).ToArray();
                foreach (ReservationTicket reservationTicket in reservationTickets)
                {
                    reservation.Tickets.Add(reservationTicket);
                    reservationTicket.Reservation = reservation;
                }
            }
            
            foreach (Ticket ticket in this.Tickets)
            {
                UserTicket[] userTickets = this.UsersTickets.Where(ut => ut.TicketID == ticket.ID).ToArray();
                foreach (UserTicket userTicket in userTickets)
                {
                    ticket.UserTicket = userTicket;
                    userTicket.Ticket = ticket;
                }
                
                ReservationTicket[] reservationTickets = this.ReservationsTickets.Where(rt => rt.TicketID == ticket.ID).ToArray();
                foreach (ReservationTicket reservationTicket in reservationTickets)
                {
                    ticket.ReservationTicket = reservationTicket;
                    reservationTicket.Ticket = ticket;
                }

                TicketType[] ticketTypes = this.TicketsTypes.Where(tt => tt.ID == ticket.TypeID).ToArray();
                foreach (TicketType ticketType in ticketTypes)
                {
                    ticket.Type = ticketType;
                    ticketType.Tickets.Add(ticket);
                }

                Flight[] flights = this.Flights.Where(f => f.ID == ticket.FlightID).ToArray();
                foreach (Flight flight in flights)
                {
                    ticket.Flight = flight;
                    flight.Tickets.Add(ticket);
                }
            }
        }

        public void ClearTablesConnection()
        {
            foreach (User user in this.Users)
            {
                user.Role = null;
                user.Tickets = null;
            }
            foreach (UserRole userRole in this.UsersRoles)
            {
                userRole.Users = null;
            }
            foreach (UserTicket userTicket in this.UsersTickets)
            {
                userTicket.User = null;
                userTicket.Ticket = null;
            }

            foreach (Reservation reservation in this.Reservations)
            {
                reservation.Tickets = null;
            }
            foreach (ReservationTicket reservationTicket in this.ReservationsTickets)
            {
                reservationTicket.Reservation = null;
                reservationTicket.Ticket = null;
            }

            foreach (Flight flight in this.Flights)
            {
                flight.Tickets = null;
            }
            foreach (TicketType ticketType in this.TicketsTypes)
            {
                ticketType.Tickets = null;
            }
            foreach (Ticket ticket in this.Tickets)
            {
                ticket.Flight = null;
                ticket.Type = null;
                ticket.UserTicket = null;
                ticket.ReservationTicket = null;
            }
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
