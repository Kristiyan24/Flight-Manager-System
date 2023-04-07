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
    public class ReservationTicketController : IConnectionTableController<ReservationTicket>
    {
        public ReservationTicketController()
        {
            dbContext = new FmDbContext();
        }
        public ReservationTicketController(FmDbContext fmDbContext)
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
        public int EntityCount() => this.FmDbContext.ReservationsTickets.Count();
        public bool EntityObjectIDsValidation(int id1, int id2)
        {
            if (id1 <= 0)
            {
                throw new ArgumentException("ReservationTicket can not have negative number or zero for Reservation ID!");
            }
            if (id1 > this.FmDbContext.Users.Count() + 1)
            {
                throw new ArgumentException("ReservationTicket Reservation ID is greater than the Last Reservation ID!");
            }
                        
            if (id2 <= 0)
            {
                throw new ArgumentException("ReservationTicket can not have negative number or zero for Ticket ID!");
            }
            if (id2 > this.FmDbContext.Tickets.Count() + 1)
            {
                throw new ArgumentException("ReservationTicket Ticket ID is greater than the Last Ticket ID!");
            }

            return true;
        }
        public bool EntityObjectValidation(ReservationTicket entity)
        {
            EntityObjectIDsValidation(entity.ReservationID, entity.TicketID);
            return true;
        }
        public bool ContainsEntity(ReservationTicket entity)
        {
            this.EntityObjectValidation(entity);
            foreach (ReservationTicket resetvationTicket in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, resetvationTicket, "Reservation", "Ticket");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(ReservationTicket entity)
        {
            this.EntityObjectValidation(entity);
            for (int i = 0; i < this.EntityCount(); i++)
            {
                ReservationTicket resetvationTicket = this.GetEntity(i);
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, resetvationTicket, "Reservation", "Ticket");
                if (equal == true)
                {
                    return i;
                }
            }

            return -1;
        }
        public ReservationTicket GetEntity(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be positive number!");
            }
            if (index < EntityCount())
            {
                throw new ArgumentException("Index must be smaller than ReservationTickets Count!");
            }
            return this.FmDbContext.ReservationsTickets.ToArray()[index];
        }
        public void AddEntity(ReservationTicket entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This ReservationTicket is awready in database!");
            }

            this.EntityObjectValidation(entity);
            this.FmDbContext.ReservationsTickets.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params ReservationTicket[] entities)
        {
            foreach (ReservationTicket resetvationTicket in entities)
            {
                this.AddEntity(resetvationTicket);
            }
        }
        public void AddEntitiesRange(IEnumerable<ReservationTicket> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<ReservationTicket> GetAllEntities()
        {
            return this.dbContext.ReservationsTickets.ToList();
        }
        public void RemoveEntity(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be positive number!");
            }
            if (index < EntityCount())
            {
                throw new ArgumentException("Index must be smaller than ReservationTickets Count!");
            }
            List<ReservationTicket> resetvationsTickets = this.GetAllEntities().ToList();
            resetvationsTickets.RemoveAt(index);
            this.RemoveAllEntities();
            foreach (ReservationTicket resetvationTicket in resetvationsTickets)
            {
                this.AddEntity(resetvationTicket);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(ReservationTicket entity)
        {
            this.EntityObjectValidation(entity);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("ReservationTicket is not in database!");
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
        public void InsertEntity(int index, ReservationTicket entity)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be positive number!");
            }
            if (index < EntityCount())
            {
                throw new ArgumentException("Index must be smaller than ReservationTickets Count!");
            }

            this.EntityObjectValidation(entity);
            List<ReservationTicket> resetvationsTickets = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            resetvationsTickets.Insert(index, entity);
            this.AddEntitiesRange(resetvationsTickets);
        }
    }
}
 
