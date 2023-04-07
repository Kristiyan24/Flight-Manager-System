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
    public class ReservationController : IMainTableController<Reservation>
    {
        public ReservationController()
        {
            dbContext = new FmDbContext();
        }
        public ReservationController(FmDbContext fmDbContext)
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
        
        
        public int EntityCount() => this.FmDbContext.Reservations.Count();
        public bool EntityObjectIDValidation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Reservation can not have negative number or zero for ID!");
            }
            if (id > this.FmDbContext.Reservations.Count() + 1)
            {
                throw new ArgumentException("Reservation ID is greater than the Last Reservation ID!");
            }
            return true;
        }
        public bool EntityObjectValidation(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Reservation can not be null!");
            }
            if (entity.FirstName.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Reservation First Name can not be null or empty!");
            }
            if (entity.MiddleName.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Reservation Middle Name can not be null or empty!");
            }
            if (entity.LastName.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Reservation Last Name can not be null or empty!");
            }
            if (entity.EGN.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Reservation EGN can not be null or empty!");
            }
            if (entity.EGN.All(char.IsDigit) == false)
            {
                throw new ArgumentException("Reservation EGN must have only digits!");
            }
            if (entity.EGN.Length != 10 )
            {
                throw new ArgumentException("Reservation EGN must have only 10 digits!");
            }
            if (entity.Mobile.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Reservation Mobile can not be null or empty!");
            }
            if (entity.Nationality.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Reservation Nationality can not be null or empty!");
            }

            return true;
        }
        public bool ContainsEntity(Reservation entity)
        {
            this.EntityObjectValidation(entity);
            foreach (Reservation reservation in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, reservation, "ID", "Tickets");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(Reservation entity)
        {
            this.EntityObjectValidation(entity);
            foreach (Reservation reservation in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, reservation, "ID", "Tickets");
                if (equal == true)
                {
                    return entity.ID - 1;
                }
            }
            return -1;
        }
        public Reservation GetEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            return this.FmDbContext.Reservations.ToArray()[id - 1];
        }
        public void AddEntity(Reservation entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This Reservation is awready in database!");
            }
            entity.ID = this.FmDbContext.Reservations.Count() + 1;
            this.FmDbContext.Reservations.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params Reservation[] entities)
        {
            foreach (Reservation reservation in entities)
            {
                this.AddEntity(reservation);
            }
        }
        public void AddEntitiesRange(IEnumerable<Reservation> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<Reservation> GetAllEntities()
        {
            return this.dbContext.Reservations.ToList();
        }
        public void RemoveEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            List<Reservation> reservations = this.GetAllEntities().ToList();
            reservations.RemoveAt(id - 1);
            this.RemoveAllEntities();
            for (int i = 1; i <= reservations.Count(); i++)
            {
                reservations[i - 1].ID = i;
                this.AddEntity(reservations[i - 1]);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(Reservation entity)
        {
            this.EntityObjectIDValidation(entity.ID);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("Reservation is not in database!");
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
        public void InsertEntity(int id, Reservation entity)
        {
            this.EntityObjectIDValidation(id);
            this.EntityObjectValidation(entity);
            List<Reservation> reservations = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            reservations.Insert(id - 1, entity);
            this.AddEntitiesRange(reservations);
        }
    }
}
 
