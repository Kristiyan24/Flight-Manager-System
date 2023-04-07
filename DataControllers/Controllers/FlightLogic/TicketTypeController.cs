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
    public class TicketTypeController : IMainTableController<TicketType>
    {
        public TicketTypeController()
        {
            dbContext = new FmDbContext();
        }
        public TicketTypeController(FmDbContext fmDbContext)
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
        public int EntityCount() => this.FmDbContext.TicketsTypes.Count();
        public bool EntityObjectIDValidation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("TicketType can not have negative number or zero for ID!");
            }
            if (id > this.FmDbContext.Flights.Count() + 1)
            {
                throw new ArgumentException("TicketType ID is greater than the Last TicketType ID!");
            }
            return true;
        }
        public bool EntityObjectValidation(TicketType entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("TicketType can not be null!");
            }
            if (entity.Name.IsNullOrEmpty())
            {
                throw new ArgumentException("TicketType Name can not be null or Empty!");
            }

            return true;
        }
        public bool ContainsEntity(TicketType entity)
        {
            this.EntityObjectValidation(entity);
            foreach (TicketType ticketType in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, ticketType, "ID", "Tickets");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(TicketType entity)
        {
            this.EntityObjectValidation(entity);
            foreach (TicketType ticketType in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, ticketType, "ID", "Tickets");
                if (equal == true)
                {
                    return entity.ID - 1;
                }
            }
            return -1;
        }
        public TicketType GetEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            return this.FmDbContext.TicketsTypes.ToArray()[id - 1];
        }
        public void AddEntity(TicketType entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This TicketType is awready in database!");
            }
            entity.ID = this.FmDbContext.TicketsTypes.Count() + 1;
            this.FmDbContext.TicketsTypes.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params TicketType[] entities)
        {
            foreach (TicketType ticketType in entities)
            {
                this.AddEntity(ticketType);
            }
        }
        public void AddEntitiesRange(IEnumerable<TicketType> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<TicketType> GetAllEntities()
        {
            return this.dbContext.TicketsTypes.ToList();
        }
        public void RemoveEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            List<TicketType> ticketTypes = this.GetAllEntities().ToList();
            ticketTypes.RemoveAt(id - 1);
            this.RemoveAllEntities();
            for (int i = 1; i <= ticketTypes.Count(); i++)
            {
                ticketTypes[i - 1].ID = i;
                this.AddEntity(ticketTypes[i - 1]);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(TicketType entity)
        {
            this.EntityObjectIDValidation(entity.ID);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("Ticket Type is not in database!");
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
        public void InsertEntity(int id, TicketType entity)
        {
            this.EntityObjectIDValidation(id);
            this.EntityObjectValidation(entity);
            List<TicketType> ticketTypes = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            ticketTypes.Insert(id - 1, entity);
            this.AddEntitiesRange(ticketTypes);
        }
    }
}
 
