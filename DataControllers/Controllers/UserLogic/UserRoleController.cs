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
    public class UserRoleController : IMainTableController<UserRole>
    {
        public UserRoleController()
        {
            dbContext = new FmDbContext();
        }
        public UserRoleController(FmDbContext fmDbContext)
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
        public int EntityCount() => this.FmDbContext.UsersRoles.Count();
        public bool EntityObjectIDValidation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("UserRole can not have negative number or zero for ID!");
            }
            if (id > this.FmDbContext.Flights.Count() + 1)
            {
                throw new ArgumentException("UserRole ID is greater than the Last UserRole ID!");
            }
            return true;
        }
        public bool EntityObjectValidation(UserRole entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("UserRole can not be null!");
            }
            if (entity.Name.IsNullOrEmpty())
            {
                throw new ArgumentException("UserRole Name can not be null or Empty!");
            }

            return true;
        }
        public bool ContainsEntity(UserRole entity)
        {
            this.EntityObjectValidation(entity);
            foreach (UserRole userRole in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, userRole, "ID", "Users");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(UserRole entity)
        {
            this.EntityObjectValidation(entity);
            foreach (UserRole userRole in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, userRole, "ID", "Users");
                if (equal == true)
                {
                    return entity.ID - 1;
                }
            }
            return -1;
        }
        public UserRole GetEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            return this.FmDbContext.UsersRoles.ToArray()[id - 1];
        }
        public void AddEntity(UserRole entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This UserRole is awready in database!");
            }
            entity.ID = this.FmDbContext.UsersRoles.Count() + 1;
            this.FmDbContext.UsersRoles.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params UserRole[] entities)
        {
            foreach (UserRole userRole in entities)
            {
                this.AddEntity(userRole);
            }
        }
        public void AddEntitiesRange(IEnumerable<UserRole> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<UserRole> GetAllEntities()
        {
            return this.dbContext.UsersRoles.ToList();
        }
        public void RemoveEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            List<UserRole> userRoles = this.GetAllEntities().ToList();
            userRoles.RemoveAt(id - 1);
            this.RemoveAllEntities();
            for (int i = 1; i <= userRoles.Count(); i++)
            {
                userRoles[i - 1].ID = i;
                this.AddEntity(userRoles[i - 1]);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(UserRole entity)
        {
            this.EntityObjectIDValidation(entity.ID);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("User Role is not in database!");
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
        public void InsertEntity(int id, UserRole entity)
        {
            this.EntityObjectIDValidation(id);
            this.EntityObjectValidation(entity);
            List<UserRole> userRoles = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            userRoles.Insert(id - 1, entity);
            this.AddEntitiesRange(userRoles);
        }
    }
}
 
