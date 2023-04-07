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
    public class UserController : IMainTableController<User>
    {
        public UserController()
        {
            dbContext = new FmDbContext();
        }
        public UserController(FmDbContext fmDbContext)
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
        
        
        public int EntityCount() => this.FmDbContext.Users.Count();
        public bool EntityObjectIDValidation(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("User can not have negative number or zero for ID!");
            }
            if (id > this.FmDbContext.Users.Count() + 1)
            {
                throw new ArgumentException("User ID is greater than the Last User ID!");
            }
            return true;
        }
        public bool EntityObjectValidation(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("User can not be null!");
            }
            if (entity.FirstName.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User First Name can not be null or empty!");
            }
            if (entity.LastName.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User Last Name can not be null or empty!");
            }
            if (entity.EGN.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User EGN can not be null or empty!");
            }
            if (entity.EGN.All(char.IsDigit) == false)
            {
                throw new ArgumentException("User EGN must have only digits!");
            }
            if (entity.EGN.Length != 10 )
            {
                throw new ArgumentException("User EGN must have only 10 digits!");
            }
            if (entity.Address.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User Address can not be null or empty!");
            }
            if (entity.Mobile.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User Mobile can not be null or empty!");
            }
            if (entity.Username.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User Username can not be null or empty!");
            }
            if (entity.Email.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User Email can not be null or empty!");
            }
            if (entity.Password.IsNullOrEmpty() == true)
            {
                throw new ArgumentException("User Password can not be null or empty!");
            }
            if (entity.RoleID <= 0)
            {
                throw new ArgumentException("User RoleID can not be negative number or zero!");
            }

            return true;
        }
        public bool ContainsEntity(User entity)
        {
            this.EntityObjectValidation(entity);
            foreach (User user in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, user, "ID", "Role", "Tickets");
                if (equal == true)
                {
                    return true;
                }
            }
            return false;
        }
        public int IndexOfEntity(User entity)
        {
            this.EntityObjectValidation(entity);
            foreach (User user in GetAllEntities())
            {
                bool equal = EntityCompararer.AreEntitiesPropertiesEqual(entity, user, "ID", "Role", "Tickets");
                if (equal == true)
                {
                    return entity.ID - 1;
                }
            }
            return -1;
        }
        public User GetEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            return this.FmDbContext.Users.ToArray()[id - 1];
        }
        public void AddEntity(User entity)
        {
            if (this.ContainsEntity(entity))
            {
                throw new ArgumentException("This User is awready in database!");
            }
            entity.ID = this.FmDbContext.Users.Count() + 1;
            this.FmDbContext.Users.Add(entity);
            this.FmDbContext.SaveChanges();
        }
        public void AddEntities(params User[] entities)
        {
            foreach (User user in entities)
            {
                this.AddEntity(user);
            }
        }
        public void AddEntitiesRange(IEnumerable<User> entities)
        {
            this.AddEntities(entities.ToArray());
        }
        public ICollection<User> GetAllEntities()
        {
            return this.dbContext.Users.ToList();
        }
        public void RemoveEntity(int id)
        {
            this.EntityObjectIDValidation(id);
            List<User> users = this.GetAllEntities().ToList();
            users.RemoveAt(id - 1);
            this.RemoveAllEntities();
            for (int i = 1; i <= users.Count(); i++)
            {
                users[i - 1].ID = i;
                this.AddEntity(users[i - 1]);
            }
            this.FmDbContext.SaveChanges();
        }
        public void RemoveEntity(User entity)
        {
            this.EntityObjectIDValidation(entity.ID);
            int index = this.IndexOfEntity(entity);
            if (index == -1)
            {
                throw new ArgumentException("User is not in database!");
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
        public void InsertEntity(int id, User entity)
        {
            this.EntityObjectIDValidation(id);
            this.EntityObjectValidation(entity);
            List<User> users = this.GetAllEntities().ToList();
            this.RemoveAllEntities();
            users.Insert(id - 1, entity);
            this.AddEntitiesRange(users);
        }
    }
}
 
