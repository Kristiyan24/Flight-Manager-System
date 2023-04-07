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
    public class UserTicketController : IConnectionTableController<UserTicket>
    {
        public UserTicketController()
        {
            dbContext = new FmDbContext();
        }
        public UserTicketController(FmDbContext fmDbContext)
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
        public int EntityCount() => this.FmDbContext.UsersTickets.Count();
        public bool EntityObjectIDsValidation(int id1, int id2)
        {
            if (id1 <= 0)
            {
                throw new ArgumentException("UserTicket can not have negative number or zero for User ID!");
            }
            if (id1 > this.FmDbContext.Users.Count() + 1)
            {
                throw new ArgumentException("UserTicket User ID is greater than the Last User ID!");
            }
                        
            if (id2 <= 0)
            {
                throw new ArgumentException("UserTicket can not have negative number or zero for Ticket ID!");
            }
            if (id2 > this.FmDbContext.Tickets.Count() + 1)
            {
                throw new ArgumentException("UserTicket Ticket ID is greater than the Last Ticket ID!");
            }

            return true;
        }

        public bool EntityObjectValidation(UserTicket entity)
        {
            EntityObjectIDsValidation(entity.UserID, entity.TicketID);
            return true;
        }

        public bool ContainsEntity(UserTicket entity)
        {
            this.EntityObjectValidation(entity);
            foreach (UserTicket userTicket in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, userTicket, "User", "Ticket");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(UserTicket entity)
        {
            this.EntityObjectValidation(entity);
            for (int i = 0; i < this.EntityCount(); i++)
            {
                UserTicket userTicket = this.GetEntity(i);
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, userTicket, "User", "Ticket");
                if (equal == true)
                {
                    return i;
                }
            }

            return -1;
        }
        public UserTicket GetEntity(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be positive number!");
            }
            if (index < EntityCount())
            {
                throw new ArgumentException("Index must be smaller than UserTickets Count!");
            }
            return this.FmDbContext.UsersTickets.ToArray()[index];
        }
        public void AddEntity(UserTicket entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This UserTicket is awready in database!");
            }

            this.EntityObjectValidation(entity);
            this.FmDbContext.UsersTickets.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params UserTicket[] entities)
        {
            foreach (UserTicket userTicket in entities)
            {
                this.AddEntity(userTicket);
            }
        }
        public void AddEntitiesRange(IEnumerable<UserTicket> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<UserTicket> GetAllEntities()
        {
            return this.dbContext.UsersTickets.ToList();
        }
        public void RemoveEntity(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be positive number!");
            }
            if (index < EntityCount())
            {
                throw new ArgumentException("Index must be smaller than UserTickets Count!");
            }
            List<UserTicket> usersTickets = this.GetAllEntities().ToList();
            usersTickets.RemoveAt(index);
            this.RemoveAllEntities();
            foreach (UserTicket userTicket in usersTickets)
            {
                this.AddEntity(userTicket);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(UserTicket entity)
        {
            this.EntityObjectValidation(entity);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("UserTicket is not in database!");
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
        public void InsertEntity(int index, UserTicket entity)
        {
            if (index < 0)
            {
                throw new ArgumentException("Index must be positive number!");
            }
            if (index < EntityCount())
            {
                throw new ArgumentException("Index must be smaller than UserTickets Count!");
            }

            this.EntityObjectValidation(entity);
            List<UserTicket> usersTickets = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            usersTickets.Insert(index, entity);
            this.AddEntitiesRange(usersTickets);
        }
    }
}
 
