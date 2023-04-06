using Bogus;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersTests
{
    public class DatabaseGenerator
    {
        public static int UsersCount = 10;
        public const int UsersRolesCount = 2; // const
        public static int UsersTicketsCount = 8;
        
        public static int ReservationsCount = 10;
        public static int ReservationsTicketsCount = 8;
        
        public static int TicketsCount = 80;
        public const int TicketsTypesCount = 2; // const
        public static int FlightsCount = 5;

        public const int StaritngID = 1;

        private Mock<FmDbContext> MockDbContext = new Mock<FmDbContext>();
        private Mock<DbSet<User>> UsersMockSet = new Mock<DbSet<User>>();
        private Mock<DbSet<UserRole>> UsersRolesMockSet = new Mock<DbSet<UserRole>>();
        private Mock<DbSet<UserTicket>> UsersTicketsMockSet = new Mock<DbSet<UserTicket>>();
        private Mock<DbSet<Reservation>> ReservationsMockSet = new Mock<DbSet<Reservation>>();
        private Mock<DbSet<ReservationTicket>> ReservationsTicketsMockSet = new Mock<DbSet<ReservationTicket>>();
        private Mock<DbSet<TicketType>> TicketsTypesMockSet = new Mock<DbSet<TicketType>>();
        private Mock<DbSet<Ticket>> TicketsMockSet = new Mock<DbSet<Ticket>>();
        private Mock<DbSet<Flight>> FlightsMockSet = new Mock<DbSet<Flight>>();

        public FmDbContext GetFmDbContext()
        {
            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertFlightsData();
            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertTicketsTypesData();
            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertTicketsData();
            Faker.GlobalUniqueIndex = StaritngID - 1;

            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertUsersRolesData();
            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertUsersData();
            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertUsersTicketsData();
            Faker.GlobalUniqueIndex = StaritngID - 1;

            InsertReservationsData();
            Faker.GlobalUniqueIndex = StaritngID - 1;
            InsertReservationsTicketsData();
            Faker.GlobalUniqueIndex = StaritngID - 1;

            return this.MockDbContext.Object;
        }

        private void InsertFlightsData()
        {
            var flightsStub = GenerateFlightsData(FlightsCount);
            var flightsData = flightsStub.AsQueryable();
            this.FlightsMockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(flightsData.Provider);
            this.FlightsMockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(flightsData.ElementType);
            this.FlightsMockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(flightsData.Expression);
            this.FlightsMockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => flightsData.GetEnumerator());
            this.MockDbContext.Setup(x => x.Flights).Returns(this.FlightsMockSet.Object);
        }

        private List<Flight> GenerateFlightsData(int count)
        {
            var faker = new Faker<Flight>()
                //.RuleFor(flight => flight.ID, faker => (faker.IndexFaker = StaritngID))
                .RuleFor(flight => flight.ID, faker => faker.IndexGlobal)
                .RuleFor(flight => flight.StartLocation, faker => faker.Address.City())
                .RuleFor(flight => flight.EndLocation, faker => faker.Address.City())
                .RuleFor(flight => flight.TakeOff, faker => faker.Date.Between(new DateTime(2000, 1, 1), new DateTime(200, 5, 5)))
                .RuleFor(flight => flight.Landing, faker => faker.Date.Between(new DateTime(2000, 5, 6), new DateTime(200, 6, 6)))
                .RuleFor(flight => flight.PlaneID, faker => faker.IndexGlobal)
                .RuleFor(flight => flight.PlaneType, faker => faker.Random.ArrayElement<string>(new string[] { "company", "military", "own" }))
                .RuleFor(flight => flight.PilotName, faker => faker.Person.FirstName)
                .RuleFor(flight => flight.AllPassengersCount, faker => faker.Random.Int(20, 50))
                .RuleFor(flight => flight.BusinessClassPassengersCount, faker => faker.Random.Int(10, 20));

            return faker.Generate(count);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void InsertTicketsTypesData()
        {
            var ticketTypesStub = GenerateTicketsTypesData();
            var ticketTypesData = ticketTypesStub.AsQueryable();
            this.TicketsTypesMockSet.As<IQueryable<TicketType>>().Setup(m => m.Provider).Returns(ticketTypesData.Provider);
            this.TicketsTypesMockSet.As<IQueryable<TicketType>>().Setup(m => m.ElementType).Returns(ticketTypesData.ElementType);
            this.TicketsTypesMockSet.As<IQueryable<TicketType>>().Setup(m => m.Expression).Returns(ticketTypesData.Expression);
            this.TicketsTypesMockSet.As<IQueryable<TicketType>>().Setup(m => m.GetEnumerator()).Returns(() => ticketTypesData.GetEnumerator());
            this.MockDbContext.Setup(x => x.TicketsTypes).Returns(this.TicketsTypesMockSet.Object);
        }

        private List<TicketType> GenerateTicketsTypesData()
        {
            return new List<TicketType>(new TicketType[] { 
                new TicketType(){ ID = StaritngID, Name = "Normal", Tickets = null },
                new TicketType(){ ID = StaritngID + 1, Name = "Business", Tickets = null }
            });
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void InsertTicketsData()
        {
            var ticketsStub = GenerateTicketsData(TicketsCount);
            var ticketsData = ticketsStub.AsQueryable();
            this.TicketsMockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(ticketsData.Provider);
            this.TicketsMockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(ticketsData.ElementType);
            this.TicketsMockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(ticketsData.Expression);
            this.TicketsMockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(() => ticketsData.GetEnumerator());
            this.MockDbContext.Setup(x => x.Tickets).Returns(this.TicketsMockSet.Object);
        }

        private List<Ticket> GenerateTicketsData(int count)
        {
            var faker = new Faker<Ticket>()
                //.RuleFor(ticket => ticket.ID, faker => (faker.IndexFaker = StaritngID))
                .RuleFor(ticket => ticket.ID, faker => faker.IndexGlobal)
                .RuleFor(ticket => ticket.TypeID, faker => faker.Random.Int(0, TicketsTypesCount))
                .RuleFor(ticket => ticket.FlightID, faker => faker.Random.Int(0, FlightsCount))
                .RuleFor(ticket => ticket.Name, (faker, ticket) => ("Ticket " + ticket.ID.ToString()))
                .RuleFor(ticket => ticket.Description, faker => faker.Lorem.Lines())
                .RuleFor(ticket => ticket.Price, faker => faker.Random.Double(0.1, 1));    

            return faker.Generate(count);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void InsertUsersRolesData()
        {
            var usersRolesStub = GenerateUsersRolesData();
            var usersRolesData = usersRolesStub.AsQueryable();
            this.UsersRolesMockSet.As<IQueryable<UserRole>>().Setup(m => m.Provider).Returns(usersRolesData.Provider);
            this.UsersRolesMockSet.As<IQueryable<UserRole>>().Setup(m => m.ElementType).Returns(usersRolesData.ElementType);
            this.UsersRolesMockSet.As<IQueryable<UserRole>>().Setup(m => m.Expression).Returns(usersRolesData.Expression);
            this.UsersRolesMockSet.As<IQueryable<UserRole>>().Setup(m => m.GetEnumerator()).Returns(() => usersRolesData.GetEnumerator());
            this.MockDbContext.Setup(x => x.UsersRoles).Returns(this.UsersRolesMockSet.Object);
        }

        private List<UserRole> GenerateUsersRolesData()
        {
            return new List<UserRole>(new UserRole[] {
                new UserRole(){ ID = StaritngID, Name = "Admin", Users = null },
                new UserRole(){ ID = StaritngID + 1, Name = "User", Users = null },
            });
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void InsertUsersData()
        {
            var userStub = GenerateUsersData(UsersCount);
            var userData = userStub.AsQueryable();
            this.UsersMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            this.UsersMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            this.UsersMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            this.UsersMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());
            this.MockDbContext.Setup(x => x.Users).Returns(this.UsersMockSet.Object);
        }

        private List<User> GenerateUsersData(int count)
        {
            var faker = new Faker<User>()
            .RuleFor(user => user.ID, faker => faker.IndexGlobal)
            .RuleFor(user => user.FirstName, faker => faker.Person.FirstName)
            .RuleFor(user => user.LastName, faker => faker.Person.LastName)
            .RuleFor(user => user.EGN, faker => faker.Random.Digits(10, 0, 9).ToString())
            .RuleFor(user => user.Address, faker => faker.Person.Address.ToString())
            .RuleFor(user => user.Mobile, faker => faker.Phone.ToString())
            .RuleFor(user => user.Username, faker => faker.Person.UserName.ToString())
            .RuleFor(user => user.Email, faker => faker.Person.Email.ToString())
            .RuleFor(user => user.Password, faker => faker.Random.Word())
            .RuleFor(user  => user.RoleID, (faker, user) => 
            {
                if (user.ID == StaritngID)
                {
                    return 0;
                }
                return 2;
            });
            return faker.Generate(count);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void InsertUsersTicketsData()
        {
            var usersTicketsStub = GenerateUsersTicketsData(UsersTicketsCount);
            var usersTicketsData = usersTicketsStub.AsQueryable();
            this.UsersTicketsMockSet.As<IQueryable<UserTicket>>().Setup(m => m.Provider).Returns(usersTicketsData.Provider);
            this.UsersTicketsMockSet.As<IQueryable<UserTicket>>().Setup(m => m.ElementType).Returns(usersTicketsData.ElementType);
            this.UsersTicketsMockSet.As<IQueryable<UserTicket>>().Setup(m => m.Expression).Returns(usersTicketsData.Expression);
            this.UsersTicketsMockSet.As<IQueryable<UserTicket>>().Setup(m => m.GetEnumerator()).Returns(() => usersTicketsData.GetEnumerator());
            this.MockDbContext.Setup(x => x.UsersTickets).Returns(this.UsersTicketsMockSet.Object);
        }

        private List<UserTicket> GenerateUsersTicketsData(int count)
        {
            FmDbContext dbContext = this.MockDbContext.Object;
            List<int> notUsedTickets = Enumerable.Range(StaritngID, (TicketsCount - StaritngID)).ToList();

            if ((dbContext.ReservationsTickets != null))
            {
                foreach (ReservationTicket reservationTicket in dbContext.ReservationsTickets)
                {
                    notUsedTickets.Remove(reservationTicket.TicketID);
                }
            }

            var faker = new Faker<UserTicket>()
            .RuleFor(userTicket => userTicket.UserID, faker => faker.Random.Int(StaritngID, (UsersCount - StaritngID)))
            .RuleFor(userTicket => userTicket.TicketID, faker =>
            {
                int ticketID = faker.Random.CollectionItem(notUsedTickets);
                notUsedTickets.Remove(ticketID);
                return ticketID;
            }
            );

            return faker.Generate(count);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void InsertReservationsData()
        {
            var reservationsStub = GenerateReservationsData(ReservationsCount);
            var reservationsData = reservationsStub.AsQueryable();
            this.ReservationsMockSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationsData.Provider);
            this.ReservationsMockSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationsData.ElementType);
            this.ReservationsMockSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationsData.Expression);
            this.ReservationsMockSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => reservationsData.GetEnumerator());
            this.MockDbContext.Setup(x => x.Reservations).Returns(this.ReservationsMockSet.Object);
        }

        private List<Reservation> GenerateReservationsData(int count)
        {
            var faker = new Faker<Reservation>()
            .RuleFor(reservation => reservation.ID, faker => faker.IndexGlobal)
            .RuleFor(reservation => reservation.FirstName, faker => faker.Person.FirstName)
            .RuleFor(reservation => reservation.MiddleName, faker => faker.Person.LastName)
            .RuleFor(reservation => reservation.LastName, faker => faker.Person.LastName)
            .RuleFor(user => user.EGN, faker => faker.Random.Digits(10, 0, 9).ToString())
            .RuleFor(user => user.Mobile, faker => faker.Phone.PhoneNumber())
            .RuleFor(user => user.Nationality, faker => faker.Address.Country());

            return faker.Generate(count);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void InsertReservationsTicketsData()
        {
            var reservationsTicketsStub = GenerateReservationsTicketsData(ReservationsTicketsCount);
            var reservationsTicketsData = reservationsTicketsStub.AsQueryable();
            this.ReservationsTicketsMockSet.As<IQueryable<ReservationTicket>>().Setup(m => m.Provider).Returns(reservationsTicketsData.Provider);
            this.ReservationsTicketsMockSet.As<IQueryable<ReservationTicket>>().Setup(m => m.ElementType).Returns(reservationsTicketsData.ElementType);
            this.ReservationsTicketsMockSet.As<IQueryable<ReservationTicket>>().Setup(m => m.Expression).Returns(reservationsTicketsData.Expression);
            this.ReservationsTicketsMockSet.As<IQueryable<ReservationTicket>>().Setup(m => m.GetEnumerator()).Returns(() => reservationsTicketsData.GetEnumerator());
            this.MockDbContext.Setup(x => x.ReservationsTickets).Returns(this.ReservationsTicketsMockSet.Object);
        }

        private List<ReservationTicket> GenerateReservationsTicketsData(int count)
        {
            FmDbContext dbContext = this.MockDbContext.Object;
            List<int> notUsedTickets = Enumerable.Range(StaritngID, (TicketsCount - StaritngID)).ToList();

            if ((dbContext.UsersTickets != null) || (!dbContext.UsersTickets.Any()))
            {
                foreach (UserTicket userTicket in dbContext.UsersTickets)
                {
                    notUsedTickets.Remove(userTicket.TicketID);
                }
            }

            var faker = new Faker<ReservationTicket>()
            .RuleFor(reservationTicket => reservationTicket.ReservationID, faker => faker.Random.Int(StaritngID, (ReservationsCount - StaritngID)))
            .RuleFor(reservationTicket => reservationTicket.TicketID, faker =>
            {
                int ticketID = faker.Random.CollectionItem(notUsedTickets);
                notUsedTickets.Remove(ticketID);
                return ticketID;
            }
            );

            return faker.Generate(count);
        }
    }
}
