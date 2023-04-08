using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class FmDbContext : IdentityDbContext<dbUser, IdentityRole, string>
    {
        public FmDbContext()
        {

        }
        public FmDbContext(DbContextOptions<FmDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)

            {

                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels; Database=Flight-Manager-Db; Integrated Security=true");

            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFlight>().HasKey(key => key.Id);

            modelBuilder.Entity<UserFlight>()
                .Property(p => p.dbUserId).ValueGeneratedNever();
            modelBuilder.Entity<UserFlight>()
                .Property(p => p.FlightId).ValueGeneratedNever();

            modelBuilder.Entity<UserFlight>()
                .HasOne(uf => uf.dbUser)
                .WithMany(uf => uf.Flights)
                .IsRequired();

            modelBuilder.Entity<UserFlight>()
                .HasOne(uf => uf.Flight)
                .WithMany(uf => uf.UsersFlights)
                .IsRequired();

            /////////////////////////////////////////////////////////////////


            modelBuilder.Entity<Reservation>()
                .HasOne(rf => rf.Flight)
                .WithMany(rf => rf.Reservations)
                .IsRequired();
            

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<UserFlight> UsersFlights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Flight> Flights { get; set; }
    }
}
