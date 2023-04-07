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
    public class FlightController : IMainTableController<Flight>
    {
        public FlightController()
        {
            dbContext = new FmDbContext();
        }
        public FlightController(FmDbContext fmDbContext)
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
        public int EntityCount() => this.FmDbContext.Flights.Count();
        public bool EntityObjectIDValidation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Flight can not have negative number or zero for ID!");
            }
            if (id > this.FmDbContext.Flights.Count() + 1)
            {
                throw new ArgumentException("Flight ID is greater than the Last Flight ID!");
            }
            return true;
        }
        public bool EntityObjectValidation(Flight entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Flight can not be null!");
            }
            if (entity.StartLocation.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Flight can not have null ot empty Start Location!");
            }
            if (entity.EndLocation.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Flight can not have null ot empty End Location!");
            }
            if (entity.StartLocation.CompareTo(entity.EndLocation) == 0)
            {
                throw new ArgumentException("Flight can not have same Start Location and End Location!");
            }
            if (entity.TakeOff == default(DateTime))
            {
                throw new ArgumentException("Flight can not have default Take Off DateTime!");
            }
            if (entity.Landing == default(DateTime))
            {
                throw new ArgumentException("Flight can not have default Landding DateTime!");

            }
            if (DateTime.Compare(entity.TakeOff, entity.Landing) > 0)
            {
                throw new ArgumentException("Flight can not have TakeOff DateTime after Landing DateTime!");
            }
            if (DateTime.Compare(entity.Landing, entity.TakeOff) < 0)
            {
                throw new ArgumentException("Flight can not have Landing DateTime before TakeOff DateTime!");
            }
            if (entity.PlaneID < 0)
            {
                throw new ArgumentException("Flight can not have plane with negative nuber or zero for PlaneID!");
            }
            if (entity.PlaneType.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Flight can not have plane with null Plane Type!");

            }
            if (entity.PilotName.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("Flight can not have plane with no Pilot - Pilot Name is null or empty!");
            }
            if (entity.AllPassengersCount <= 0)
            {
                throw new ArgumentException("Flight can not have plane with negative nuber or zero for Passengers Count!");
            }
            if (entity.BusinessClassPassengersCount < 0)
            {
                throw new ArgumentException("Flight can not have plane with negative number for Business Class Passengers Count!");
            }
            if (entity.AllPassengersCount < entity.BusinessClassPassengersCount)
            {
                throw new ArgumentException("Flight can not have plane with more Business Class Passengers Count than All Passengers Count!");
            }
            return true;
        }
        public bool ContainsEntity(Flight entity)
        {
            this.EntityObjectValidation(entity);
            foreach (Flight flight in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, flight, "ID", "Tickets");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(Flight entity)
        {
            this.EntityObjectValidation(entity);
            foreach (Flight flight in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, flight, "ID", "Tickets");
                if (equal == true)
                {
                    return entity.ID - 1;
                }
            }
            return -1;
        }
        public Flight GetEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            return this.FmDbContext.Flights.ToArray()[id - 1];
        }
        public void AddEntity(Flight entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This flight is awready in database!");
            }
            entity.ID = this.FmDbContext.Flights.Count() + 1;
            this.FmDbContext.Flights.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params Flight[] entities)
        {
            foreach (Flight flight in entities)
            {
                this.AddEntity(flight);
            }
        }
        public void AddEntitiesRange(IEnumerable<Flight> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<Flight> GetAllEntities()
        {
            return this.dbContext.Flights.ToList();
        }
        public void RemoveEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            List<Flight> flights = this.GetAllEntities().ToList();
            flights.RemoveAt(id - 1);
            this.RemoveAllEntities();
            for (int i = 1; i <= flights.Count(); i++)
            {
                flights[i - 1].ID = i;
                this.AddEntity(flights[i - 1]);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(Flight entity)
        {
            this.EntityObjectIDValidation(entity.ID);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("Flight is not in database!");
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
        public void InsertEntity(int id, Flight entity)
        {
            this.EntityObjectIDValidation(id);
            this.EntityObjectValidation(entity);
            List<Flight> flights = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            flights.Insert(id - 1, entity);
            this.AddEntitiesRange(flights);
        }
    }
}
 
