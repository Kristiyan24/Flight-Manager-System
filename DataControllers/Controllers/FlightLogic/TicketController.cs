using Castle.Core.Internal;
using Data;
using Data.Controllers;
using Data.Entities;
using Data.EntitiesComparers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{
    public class TicketController : IMainTableController<Ticket>
    {
        public TicketController()
        {
            dbContext = new FmDbContext();
        }
        public TicketController(FmDbContext fmDbContext)
        {
            this.FmDbContext = dbContext;
        }       
        
        
        private FmDbContext dbContext;
        public FmDbContext FmDbContext
        {
            get { return this.dbContext; }
            set
            {
                if (dbContext == null)
                {
                    throw new Exception("FmDbContext can not be null!");
                }
                this.dbContext = value;
            }
        }
        public int EntityCount() => this.FmDbContext.Tickets.Count();
        public bool EntityObjectIDValidation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Ticket can not have negative number or zero for ID!");
            }
            if (id > this.FmDbContext.Flights.Count() + 1)
            {
                throw new ArgumentException("Ticket ID is greater than the Last Ticket ID!");
            }
            return true;
        }
        public bool EntityObjectValidation(Ticket entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Ticket can not be null!");
            }
            if (entity.TypeID <= 0)
            {
                throw new ArgumentException("Ticket can not have negative numer or zero for TypeID!");
            }

            if (entity.FlightID <= 0)
            {
                throw new ArgumentException("Ticket can not have negative numer or zero for FlightID!");
            }
            if (entity.Name.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Ticket Name can not be null or empty!");
            }
            if (entity.Description.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Ticket Description can not be null or empty!");
            }
            if (entity.Price < 0)
            {
                throw new ArgumentException("Ticket Price can not be negative numer!");
            }

            bool IsUserAndReservationTicket = (entity.UserTicketID != null) && (entity.ReservationTicketID != null);
            if (IsUserAndReservationTicket == true)
            {
                throw new ArgumentException("Ticket must own only from one User or one Reservation - not both!");
            }

            return true;
        }
        public bool ContainsEntity(Ticket entity)
        {
            this.EntityObjectValidation(entity);
            foreach (Ticket ticket in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, ticket, "ID", "Type", "Flight", "UserTicket", "ReservationTicket");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(Ticket entity)
        {
            this.EntityObjectValidation(entity);
            foreach (Ticket ticket in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, ticket, "ID", "Type", "Flight", "UserTicket", "ReservationTicket");
                if (equal == true)
                {
                    return entity.ID - 1;
                }
            }
            return -1;
        }
        public Ticket GetEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            return this.FmDbContext.Tickets.ToArray()[id - 1];
        }
        public void AddEntity(Ticket entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This flight is awready in database!");
            }
            entity.ID = this.FmDbContext.Tickets.Count() + 1;
            this.FmDbContext.Tickets.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params Ticket[] entities)
        {
            foreach (Ticket flight in entities)
            {
                this.AddEntity(flight);
            }
        }
        public void AddEntitiesRange(IEnumerable<Ticket> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<Ticket> GetAllEntities()
        {
            return this.dbContext.Tickets.ToList();
        }
        public void RemoveEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            List<Ticket> flights = this.GetAllEntities().ToList();
            flights.RemoveAt(id - 1);
            this.RemoveAllEntities();
            for (int i = 1; i <= flights.Count(); i++)
            {
                flights[i - 1].ID = i;
                this.AddEntity(flights[i - 1]);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(Ticket entity)
        {
            this.EntityObjectIDValidation(entity.ID);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("Ticket is not in database!");
            }
            this.RemoveEntity(index + 1);
        }
        public void RemoveAllEntities()
        {
            foreach (var entity in GetAllEntities())
            {
                this.FmDbContext.Remove(entity);
            }
            this.FmDbContext.SaveChanges();
        }     
        public void InsertEntity(int id, Ticket entity)
        {
            this.EntityObjectIDValidation(id);
            this.EntityObjectValidation(entity);
            List<Ticket> flights = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            flights.Insert(id - 1, entity);
            this.AddEntitiesRange(flights);
        }
    }
}
 
