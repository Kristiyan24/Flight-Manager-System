using Bogus;
using Data;
using Data.Controllers;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ControllersTests
{
    [TestClass]
    public class DatabaseGeneratorTests
    {
        [TestMethod]
        public void TableFlightsTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Flights:");
            foreach (var f in dbContext.Flights)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(f))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(f)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableTicketsTypesTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Tickets Types:");
            foreach (var tt in dbContext.TicketsTypes)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(tt))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(tt)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableTicketsTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Tickets");
            foreach (var t in dbContext.Tickets)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(t))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(t)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableUsersRolesTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Users Roles:");
            foreach (var ur in dbContext.UsersRoles)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(ur))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(ur)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableUsersTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Users:");
            foreach (var u in dbContext.Users)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(u))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(u)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableUsersTicketsTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Users Tickets:");
            foreach (var ut in dbContext.UsersTickets)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(ut))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(ut)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableReservationsTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Reservations:");
            foreach (var r in dbContext.Reservations)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(r))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(r)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        [TestMethod]
        public void TableReservationsTicketsTest()
        {
            DatabaseGenerator databaseGenerator = new DatabaseGenerator();
            FmDbContext dbContext = databaseGenerator.GetFmDbContext();

            System.Diagnostics.Debug.WriteLine("Reservations Tickets:");
            foreach (var rt in dbContext.ReservationsTickets)
            {
                foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(rt))
                {
                    System.Diagnostics.Debug.WriteLine($"{p.Name}: {p.GetValue(rt)}");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}
