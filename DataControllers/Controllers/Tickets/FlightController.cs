using Castle.Core.Internal;
using Data;
using Data.Controllers;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataControllers.Controllers.Tickets
{
    class FlightController : IController<Flight>
    {
        private FmDbContext dbContext;
        private static int IDCounter = 1;

        public FlightController()
        {
            dbContext = new FmDbContext();
        }

        public FlightController(FmDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool EntityValidation(Flight entity)
        {
            if (entity == null)
            {
                throw new Exception("Flight can not be null!");
            }
            if (entity.ID < 0)
            {
                throw new Exception("Flight can not have negative ID!");
            }
            if (entity.StartLocation.IsNullOrEmpty() == true)
            {
                throw new Exception("Flight can not have null ot empty Start Location!");
            }
            if (entity.EndLocation.IsNullOrEmpty() == true)
            {
                throw new Exception("Flight can not have null ot empty Start Location!");
            }
            if (entity.TakeOff == default(DateTime))
            {
                throw new Exception("Flight can not have null TakeOFF DateTime!");
            }
            if (entity.Landing == default(DateTime))
            {
                throw new Exception("Flight can not have null Landding DateTime!");
            }
            if (DateTime.Compare(entity.TakeOff, entity.Landing) > 0)
            {
                throw new Exception("Flight can not have TakeOff DateTime after Landing DateTime!");
            }
            if (DateTime.Compare(entity.Landing, entity.TakeOff) < 0)
            {
                throw new Exception("Flight can not have Landing DateTime before TakeOff DateTime!");
            }
            if (entity.PlaneID < 0)
            {
                throw new Exception("Flight can not have plane with negative ID!");
            }
            if (entity.PlaneType.IsNullOrEmpty() == true)
            {
                throw new Exception("Flight can not have plane with null Plane Type!");
            }
            if (entity.PilotName.IsNullOrEmpty() == true)
            {
                throw new Exception("Flight can not have plane with no pilot!");
            }
            if (entity.AllPassengersCount < 0)
            {
                throw new Exception("Flight can not have plane with negative Passengers Count!");
            }
            if (entity.BusinessClassPassengersCount < 0)
            {
                throw new Exception("Flight can not have plane with negative Business Class Passengers Count!");
            }
            if (entity.AllPassengersCount < entity.BusinessClassPassengersCount)
            {
                throw new Exception("Flight can not have plane with more Business Class Passengers Count than All Passengers Count!");
            }
            return true;
        }
        public void Add(Flight entity)
        {
            this.EntityValidation(entity);
            entity.ID = IDCounter;
            IDCounter++;
            this.dbContext.Flights.Add(entity);
        }
        public void Add(params Flight[] entities)
        {
            foreach (Flight flight in entities)
            {
                this.Add(flight);
            }
        }
        public void AddRange(IEnumerable<Flight> collection)
        {
            this.Add(collection.ToArray());
        }
        public void Insert(int index, Flight entity)
        {
            this.EntityValidation(entity);
            IDCounter++;

            List<Flight> flights = this.GetAll().ToList();
            for (int i = 0; i <= index; i++)
            {

            }
            
        }

        public void InsertRange(int index, params Flight[] entities)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(int index, IEnumerable<Flight> collection)
        {
            throw new NotImplementedException();
        }




        public bool Contains(Flight entity)
        {
            throw new NotImplementedException();
        }

        

        public ICollection<Flight> GetAll()
        {
            return this.dbContext.Flights.ToList();
        }

        public Flight GetAt(int index)
        {
            throw new NotImplementedException();
        }

        public void IDsValidation()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(Flight entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Flight item)
        {
            throw new NotImplementedException();
        }

        public int RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(int index, int count)
        {
            throw new NotImplementedException();
        }
    }
}
